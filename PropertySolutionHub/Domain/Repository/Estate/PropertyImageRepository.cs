using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertySolutionHub.Domain.Repository.Estate
{
    public interface IPropertyImageRepository : IDynamicDbRepository
    {
        Task<int> CreatePropertyImage(IFormFile file, string name, int Id);
        Task<bool> DeletePropertyImage(int Id);
        Task<List<PropertyImage>> GetPropertyImageByPropertyId(int Id);
        Task<List<PropertyImage>> GetPropertyImageByRemotePropertyId(int Id);
        Task<List<PropertyImage>> GetAllPropertyImages();
        Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage);
        void Validate(PropertyImage propertyImage);
        string UploadImage(IFormFile file, int Id);
    }

    public class PropertyImageRepository : AbstractDynamicDbRepository, IPropertyImageRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        private readonly DapperContext _dapperContext;
        private readonly string DomainKey = string.Empty;
        IHttpContextAccessor _httpContextAccessor;
        IStorageHelper storageHelper;


        public const string Images = "Images";
        public const string ModelName = "Property";

        public PropertyImageRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper, IHttpContextAccessor httpContextAccessor, IStorageHelper storageHelper) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            _dapperContext = dapperContext;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _httpHelper = httpHelper;
            DomainKey = GetDomainKey();
            _httpContextAccessor = httpContextAccessor;
            this.storageHelper = storageHelper;
        }

        public void Validate(PropertyImage propertyImage)
        {
            ValidationHelper.CheckIsNull(propertyImage);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(propertyImage.Name), "Image name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(propertyImage.ImageUrl), "Image URL is required.");
        }

        public string UploadImage(IFormFile file, int Id)
        {
            try
            {
                string uploadedImageFileUrl = string.Empty;
                storageHelper.UploadImage(Id, storageHelper.GetStoragePath(DomainKey.ToString(), Images, ModelName), storageHelper.GetUrlPath(_httpContextAccessor.HttpContext, DomainKey, Images, ModelName), ModelName, file, true, ref uploadedImageFileUrl);

                if (!string.IsNullOrEmpty(uploadedImageFileUrl))
                    return uploadedImageFileUrl;
            }
            catch (Exception e)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public async Task<int> CreatePropertyImage(IFormFile file, string name, int Id)
        {
            try
            {
                string uploadedImageFileUrl = string.Empty;
                storageHelper.UploadImage(Id, storageHelper.GetStoragePath(DomainKey.ToString(), Images, ModelName), storageHelper.GetUrlPath(_httpContextAccessor.HttpContext, DomainKey, Images, ModelName), ModelName, file, false, ref uploadedImageFileUrl); ;
             
                if (!string.IsNullOrEmpty(uploadedImageFileUrl))
                {
                    PropertyImage propertyImage = new PropertyImage
                    {
                        Name = name,
                        ImageUrl = uploadedImageFileUrl,
                        PropertyId = Id,
                        CreatedDate = DateTime.Now
                    };

                    db.PropertyImages.Add(propertyImage);
                    await db.SaveChangesAsync();
                }
                    return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding property image. " + ex.Message);
            }
        }

        public async Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage)
        {
            try
            {
                Validate(propertyImage);

                string updateQuery = @"
                    UPDATE PropertyImages SET
                    Name = @Name, ImageUrl = @ImageUrl, PropertyId = @PropertyId, ModifiedDate = @ModifiedDate
                    WHERE Id = @Id";

                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(updateQuery, propertyImage);

                return propertyImage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating property image. " + ex.Message);
            }
        }

        public async Task<bool> DeletePropertyImage(int Id)
        {
            try
            {
                PropertyImage propertyImage = db.PropertyImages.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                Uri uri = new Uri(propertyImage.ImageUrl);
                string lastSegment = uri.Segments.Last().TrimEnd('/');
                bool deleted = storageHelper.DeleteFromStorage(storageHelper.GetStoragePath(DomainKey.ToString(), Images, ModelName), lastSegment);

                if (deleted)
                {
                    propertyImage.ArchiveDate = DateTime.Now;
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property image. " + ex.Message);
            }
        }

        public async Task<List<PropertyImage>> GetAllPropertyImages()
        {
            try
            {
                string sqlQuery = "SELECT * FROM PropertyImages WHERE ArchiveDate IS NULL";

                using var connection = _dapperContext.CreateConnection();
                var propertyImages = await connection.QueryAsync<PropertyImage>(sqlQuery);
                return propertyImages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property images. " + ex.Message);
            }
        }

        public async Task<List<PropertyImage>> GetPropertyImageByPropertyId(int Id)
        {
            try
            {
                return await db.PropertyImages.Where(m => m.PropertyId == Id && m.ArchiveDate == null).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property image. " + ex.Message);
            }
        }

        public async Task<List<PropertyImage>> GetPropertyImageByRemotePropertyId(int Id)
        {
            try
            {
                return await db.PropertyImages.Where(m => m.Property.RemoteId == Id && m.ArchiveDate == null).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving property image. " + ex.Message);
            }
        }
    }
}
