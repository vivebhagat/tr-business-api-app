using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Auth;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IBusinessUserToDomainKeyMapRepository
    {
        Task<int> CreateBusinessUserToDomainKeyMap(BusinessUserToDomainKeyMap businessUserToDomainKeyMap);
        Task<bool> DeleteBusinessUserToDomainKeyMap(int businessUserToDomainKeyMapId);
        Task<BusinessUserToDomainKeyMap> GetBusinessUserToDomainKeyMapById(int businessUserToDomainKeyMapId);
        Task<List<BusinessUserToDomainKeyMap>> GetAllBusinessUserToDomainKeyMaps();
        Task<BusinessUserToDomainKeyMap> UpdateBusinessUserToDomainKeyMap(BusinessUserToDomainKeyMap businessUserToDomainKeyMap);
        Task<List<BusinessUserToDomainKeyMap>> GetDomainKeyForUsers(string Id);
        Task<DomainValidationResponse> SelectDomain(int Id);
    }

    public class BusinessUserToDomainKeyMapRepository : IBusinessUserToDomainKeyMapRepository
    {
        ILocalDbContext db;
        private readonly IMemoryCache _cache;

        public BusinessUserToDomainKeyMapRepository(ILocalDbContext db, IMemoryCache cache)
        {
            this.db = db;
            this._cache = cache;
        }

        public void Validate(BusinessUserToDomainKeyMap businessUserToDomainKeyMap)
        {
            ValidationHelper.CheckIsNull(businessUserToDomainKeyMap);
        }

        public async Task<int> CreateBusinessUserToDomainKeyMap(BusinessUserToDomainKeyMap businessUserToDomainKeyMap)
        {
            try
            {
                Validate(businessUserToDomainKeyMap);
                businessUserToDomainKeyMap.CreatedDate = DateTime.Now;
                //db.BusinessUserToDomainKeyMaps.Add(businessUserToDomainKeyMap);
                await db.SaveChangesAsync();

                return businessUserToDomainKeyMap.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding data. " + ex.Message);
            }
        }

        public async Task<BusinessUserToDomainKeyMap> UpdateBusinessUserToDomainKeyMap(BusinessUserToDomainKeyMap businessUserToDomainKeyMap)
        {
            try
            {
                Validate(businessUserToDomainKeyMap);
                businessUserToDomainKeyMap.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(businessUserToDomainKeyMap);
                await db.SaveChangesAsync();
                return businessUserToDomainKeyMap;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating data. " + ex.Message);
            }
        }

        public async Task<bool> DeleteBusinessUserToDomainKeyMap(int businessUserToDomainKeyMapId)
        {
            try
            {
                BusinessUserToDomainKeyMap businessUserToDomainKeyMap = db.BusinessUserToDomainKeyMaps.Where(m => m.Id == businessUserToDomainKeyMapId && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("BusinessUserToDomainKeyMap details not found.");
                businessUserToDomainKeyMap.ModifiedDate = DateTime.Now;
                businessUserToDomainKeyMap.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting data. " + ex.Message);
            }
        }

        public async Task<List<BusinessUserToDomainKeyMap>> GetAllBusinessUserToDomainKeyMaps()
        {
            return await db.BusinessUserToDomainKeyMaps.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<BusinessUserToDomainKeyMap> GetBusinessUserToDomainKeyMapById(int businessUserToDomainKeyMapId)
        {
            return await db.BusinessUserToDomainKeyMaps.Where(m => m.Id == businessUserToDomainKeyMapId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<List<BusinessUserToDomainKeyMap>> GetDomainKeyForUsers(string Id)
        {
            try
            {
                int businessUserId = db.BusinessUsers.Where(m => m.UserId == Id && m.ArchiveDate == null).Select(m => m.Id).FirstOrDefault();
                List<BusinessUserToDomainKeyMap> domainKeys = await db.BusinessUserToDomainKeyMaps.Where(m => m.BusinessUserId == businessUserId && m.ArchiveDate == null).ToListAsync();
                return domainKeys;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving domain keys. " + ex.Message);
            }
        }

        private string GetCacheKey()
        {
            return $"DomainKeyDetails";
        }

        public async Task<DomainValidationResponse> SelectDomain(int Id)
        {
            BusinessUserToDomainKeyMap businessUserToDomainKeyMap = db.BusinessUserToDomainKeyMaps.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();
            DomainToDataStoreMap dataStore = db.DomainKeyToDataStoreMaps.Where(m => m.DomainKeyId == businessUserToDomainKeyMap.DomainKeyId && m.ArchiveDate == null).FirstOrDefault();


            if (businessUserToDomainKeyMap == null)
                throw new Exception("Invalid domain details.");

            if (_cache.TryGetValue<DomainValidationResponse>(GetCacheKey(), out var cachedResponse))
            {
                _cache.Remove(GetCacheKey());
            }

            var result = new DomainValidationResponse
            {
                UserId = businessUserToDomainKeyMap.BusinessUser.UserId,
                Domain = businessUserToDomainKeyMap.DomainKey.Name,
                SubDomain = businessUserToDomainKeyMap.DomainKey.SubDomain,
                DomainKey = businessUserToDomainKeyMap.DomainKey.Value,
                DomainKeyId = businessUserToDomainKeyMap.DomainKeyId
            };


            _cache.Set(GetCacheKey(), result, DateTimeOffset.Now.AddYears(100));

            return result;
        }
    }
}
