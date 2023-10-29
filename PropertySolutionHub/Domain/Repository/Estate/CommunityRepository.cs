using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace CommunitySolutionHub.Domain.Repository.Estate
{
    public interface ICommunityRepository : IDynamicDbRepository
    {
        Task<int> CreateCommunity(Community community, IFormFile file);
        Task<int> DeleteCommunity(int Id);
        Task<bool> DeleteRemoteCommunity(int remoteId);
        Task<Community> GetCommunityById(int Id);
        Task<List<Community>> GetAllCommunities();
        Task<Community> UpdateCommunity(Community community, IFormFile file);
        Task<Community> UpdateRemoteCommunity(string postData);
        Task<bool> UpdateRemoteId(string postData, int Id);
        Task<List<Community>> GetAllFeaturedCommunities();
    }

    public class CommunityRepository : AbstractDynamicDbRepository, ICommunityRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        private readonly string DomainKey = string.Empty;
        IStorageHelper storageHelper;
        IHttpContextAccessor _httpContextAccessor;

        public CommunityRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper, IStorageHelper storageHelper, IHttpContextAccessor httpContextAccessor) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _httpHelper = httpHelper;
            this.storageHelper = storageHelper;
            DomainKey = GetDomainKey();
            _httpContextAccessor = httpContextAccessor;
        }

        public void Validate(Community community)
        {
            ValidationHelper.CheckIsNull(community);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(community.Name), "Name is required.");
            ValidationHelper.CheckException(community.OrganizationId == 0, "Organization is requried.");
            ValidationHelper.CheckException(community.StatusId == 0, "Construction status is required.");
            ValidationHelper.CheckException(community.CommunityTypeId == 0, "Community type is required.");
            ValidationHelper.CheckException(community.LandArea <= 0, "Land area is required.");
        }

        public async Task<int> CreateCommunity(Community community, IFormFile file)
        {

            try
            {
                Validate(community);

                if (db.Communities.Any(m => m.Name == community.Name && m.ArchiveDate == null))
                {
                    throw new Exception("A community with the same name already exists.");
                }

                community.CreatedDate = DateTime.Now;
                db.Communities.Add(community);
                int rowsAffected = await db.SaveChangesAsync();

                if (file != null)
                {
                    string uploadedImageFileUrl = string.Empty;
                    storageHelper.UploadImage(community.Id, storageHelper.GetStoragePath(DomainKey.ToString(), "Images", "Community"), storageHelper.GetUrlPath(_httpContextAccessor.HttpContext, DomainKey, "Images", "Community"), "Community", file, true, ref uploadedImageFileUrl);

                    if (!string.IsNullOrEmpty(uploadedImageFileUrl))
                    {
                        community.Url = uploadedImageFileUrl;
                        db.SaveChanges();
                    }
                }

                return community.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding community. " + ex.Message);
            }
        }

        public async Task<bool> UpdateRemoteId(string postData, int Id)
        {
            Community community = db.Communities.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();
            int result = await _httpHelper.PostAsync<int>(postData, DomainKey, "/api/CommunityExternal/AddCommunity");

            if (result != 0)
            {
                community.RemoteId = result;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<Community> UpdateCommunity(Community community, IFormFile file)
        {
            try
            {
                Validate(community);
                community.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(community);
                await db.SaveChangesAsync();

                if (file != null)
                {
                    string uploadedImageFileUrl = string.Empty;
                    storageHelper.UploadImage(community.Id, storageHelper.GetStoragePath(DomainKey.ToString(), "Images", "Community"), storageHelper.GetUrlPath(_httpContextAccessor.HttpContext, DomainKey, "Images", "Community"), "Community", file, true, ref uploadedImageFileUrl);

                    if (!string.IsNullOrEmpty(uploadedImageFileUrl))
                    {
                        community.Url = uploadedImageFileUrl;
                        db.SaveChanges();
                    }
                }
                return community;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating community. " + ex.Message);
            }
        }

        public async Task<int> DeleteCommunity(int Id)
        {
            try
            {
                Community community = db.Communities.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (community == null)
                    throw new Exception("Community does not exist.");

                community.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return community.RemoteId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting community. " + ex.Message);
            }
        }

        public async Task<List<Community>> GetAllCommunities()
        {
            return await db.Communities.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<List<Community>> GetAllFeaturedCommunities()
        {
            return await db.Communities.Where(m => m.IsFeatured && m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Community> GetCommunityById(int Id)
        {
            return await db.Communities.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRemoteCommunity(int remoteId)
        {
            var deleted = await _httpHelper.GetAsync<bool>("/api/CommunityExternal/DeleteCommunity/" + remoteId, DomainKey);
            return deleted;
        }

        public async Task<Community> UpdateRemoteCommunity(string postData)
        {
            var result = await _httpHelper.PostAsync<Community>(postData, DomainKey, "/api/CommunityExternal/EditCommunity");
            return result;
        }
    }
}
