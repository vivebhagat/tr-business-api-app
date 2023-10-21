using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Setup;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace OrganizationSolutionHub.Domain.Repository.Estate
{
    public interface IOrganizationRepository : IDynamicDbRepository
    {
        Task<int> CreateOrganization(Organization Organization);
        Task<int> DeleteOrganization(int Id);
        Task<Organization> GetOrganizationById(int Id);
        Task<Organization> UpdateOrganization(Organization Organization, IFormFile file);
        Task<List<Organization>> GetAllOrganizations();
    }

    public class OrganizationRepository : AbstractDynamicDbRepository, IOrganizationRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        private readonly string DomainKey = string.Empty;
        IHttpContextAccessor _httpContextAccessor;
        IStorageHelper _storageHelper;

        public OrganizationRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpContextAccessor httpContextAccessor, IStorageHelper storageHelper) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            DomainKey = GetDomainKey();
            _httpContextAccessor = httpContextAccessor;
            _storageHelper = storageHelper;
        }

        public void Validate(Organization Organization)
        {
            ValidationHelper.CheckIsNull(Organization);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(Organization.Name), "Name is required.");
        }

        public async Task<int> CreateOrganization(Organization Organization)
        {
            try
            {
                Validate(Organization);

                if (db.Organizations.Any(m => m.Name == Organization.Name && m.ArchiveDate == null))
                {
                    throw new Exception("A Organization with the same name already exists.");
                }

                Organization.CreatedDate = DateTime.Now;
                db.Organizations.Add(Organization);
                int rowsAffected = await db.SaveChangesAsync();
                return Organization.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Organization. " + ex.Message);
            }
        }

        public async Task<Organization> UpdateOrganization(Organization Organization, IFormFile file)
        {
            try
            {
                Validate(Organization);
                Organization.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(Organization);
                await db.SaveChangesAsync();

                if (file != null)
                {
                    string uploadedImageFileUrl = string.Empty;
                    _storageHelper.UploadImage(Organization.Id, _storageHelper.GetStoragePath(DomainKey.ToString(), "Logo", "Org"), _storageHelper.GetUrlPath(_httpContextAccessor.HttpContext, DomainKey, "Logo", "Org"), "Org", file, true, ref uploadedImageFileUrl);

                    if (!string.IsNullOrEmpty(uploadedImageFileUrl))
                    {
                        Organization.Url = uploadedImageFileUrl;
                        db.SaveChanges();
                    }
                }
                return Organization;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Organization. " + ex.Message);
            }
        }

        public async Task<int> DeleteOrganization(int Id)
        {
            try
            {
                Organization Organization = db.Organizations.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (Organization == null)
                    throw new Exception("Organization does not exist.");

                Organization.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Organization. " + ex.Message);
            }
        }

        public async Task<List<Organization>> GetAllOrganizations()
        {
            return await db.Organizations.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Organization> GetOrganizationById(int Id)
        {
            return await db.Organizations.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
