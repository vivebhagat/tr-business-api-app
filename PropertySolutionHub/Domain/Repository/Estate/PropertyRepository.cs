using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Estate
{
    public interface IPropertyRepository : IDynamicDbRepository
    {
        Task<int> CreateProperty(Property property, IFormFile file);
        Task<int> DeleteProperty(int Id);
        Task<bool> DeleteRemoteProperty(int remoteId);
        Task<Property> GetPropertyById(int Id);
        Task<List<Property>> GetAllProperties();
        Task<Property> UpdateProperty(Property property, IFormFile file);
        Task<Property> UpdateRemoteProperty(string postData);
        Task<bool> UpdateRemoteId(string postData, int Id);
        Task<List<Property>> GetAllFeaturedProperties();
    }

    public class PropertyRepository : AbstractDynamicDbRepository, IPropertyRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly string DomainKey = string.Empty;

        public PropertyRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper, IPropertyImageRepository propertyImageRepository) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _httpHelper = httpHelper;
            DomainKey = GetDomainKey();
            _propertyImageRepository = propertyImageRepository;
        }

        public void Validate(Property property)
        {
            ValidationHelper.CheckIsNull(property);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(property.Name), "Name is required.");
            ValidationHelper.CheckException(property.Price <= 0, "Price should be a positive value.");
            ValidationHelper.ValidateEnum(property.Type, "Type");
            ValidationHelper.ValidateEnum(property.Status, "Status");
            ValidationHelper.CheckException(property.Area <= 0, "Area should be a positive value.");
        }

        public async Task<int> CreateProperty(Property property, IFormFile file)
        {

            try
            {
                Validate(property);

                if (db.Property.Any(m => m.Name == property.Name && m.ArchiveDate == null))
                {
                    throw new Exception("A property with the same name already exists.");
                }

                property.CreatedDate = DateTime.Now;
                db.Property.Add(property);
                int rowsAffected = await db.SaveChangesAsync();

                if(file != null)
                {
                    var imageUrl = _propertyImageRepository.UploadImage(file, property.Id);
                    property.Url = imageUrl;
                    db.SaveChanges();
                }

                return property.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding property. " + ex.Message);
            }
        }

        public async Task<bool> UpdateRemoteId(string postData, int Id)
        {
            Property property = db.Property.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();
            int result = await _httpHelper.PostAsync<int>(postData, DomainKey, "/api/PropertyExternal/AddProperty");

            if (result != 0)
            {
                property.RemoteId = result;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<Property> UpdateProperty(Property property, IFormFile file)
        {
            try
            {
                Validate(property);
                property.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(property);
                await db.SaveChangesAsync();


                if (file != null)
                {
                    var imageUrl = _propertyImageRepository.UploadImage(file, property.Id);
                    property.Url = imageUrl;
                    db.SaveChanges();
                }

                return property;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating property. " + ex.Message);
            }
        }

        public async Task<int> DeleteProperty(int Id)
        {
            try
            {
                Property property = db.Property.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (property == null)
                    throw new Exception("Property does not exist.");

                property.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return property.RemoteId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property. " + ex.Message);
            }
        }

        public async Task<List<Property>> GetAllProperties()
        {
            return await db.Property.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<List<Property>> GetAllFeaturedProperties()
        {
            return await db.Property.Where(m => m.IsFeatured && m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Property> GetPropertyById(int Id)
        {
            return await db.Property.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRemoteProperty(int remoteId)
        {
            var deleted = await _httpHelper.GetAsync<bool>("/api/PropertyExternal/DeleteProperty/" + remoteId, DomainKey);
            return deleted;
        }

        public async Task<Property> UpdateRemoteProperty(string postData)
        {
            var result = await _httpHelper.PostAsync<Property>(postData, DomainKey, "/api/PropertyExternal/EditProperty");
            return result;
        }
    }
}
