using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Auth
{
    public interface IBaseApplicationUserToDomainKeyMapRepository
    {
        Task<List<BaseApplicationUserToDomainKeyMap>> GetDomainKeyForUsers(string Id);
        Task<DomainDetail> SelectDomain(int Id);
        void Validate(BaseApplicationUserToDomainKeyMap @object);
    }

    public class BaseApplicationUserToDomainKeyMapRepository : IBaseApplicationUserToDomainKeyMapRepository
    {
        IAuthDbContext _authDb;
        private readonly IMemoryCache _cache;

        public BaseApplicationUserToDomainKeyMapRepository(IAuthDbContext authDb, IMemoryCache cache)
        {
            _authDb = authDb;
            this._cache = cache;
        }

        public void Validate(BaseApplicationUserToDomainKeyMap @object)
        {
            ValidationHelper.CheckIsNull(@object);
        }

        public async Task<List<BaseApplicationUserToDomainKeyMap>> GetDomainKeyForUsers(string Id)
        {
            try
            {
                List<BaseApplicationUserToDomainKeyMap> domainKeyList = _authDb.BaseApplicationUserToDomainKeyMaps.Where(m => m.BaseApplicationUserId == Id && m.ArchiveDate == null).ToList();
                return domainKeyList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving domain keys. " + ex.Message);
            }
        }

        private string GetCacheKey(int Id)
        {
            return $"DomainKeyDetails_{Id}";
        }

        public async Task<DomainDetail> SelectDomain(int Id)
        {
            if (_cache.TryGetValue<DomainDetail>(GetCacheKey(Id), out var cachedResponse))
            {
                return cachedResponse;
            }

            BaseApplicationUserToDomainKeyMap baseApplicationUserToDomainKeyMap = _authDb.BaseApplicationUserToDomainKeyMaps.Where(m => m.DomainKey.Value == Id && m.ArchiveDate == null).FirstOrDefault();

            if (baseApplicationUserToDomainKeyMap == null)
                throw new Exception("Invalid domain details.");

            var result = new DomainDetail
            {
                UserId = baseApplicationUserToDomainKeyMap.BaseApplicationUserId,
                Domain = baseApplicationUserToDomainKeyMap.DomainKey.Name,
                SubDomain = baseApplicationUserToDomainKeyMap.DomainKey.SubDomain,
                DomainKey = baseApplicationUserToDomainKeyMap.DomainKey.Value,
                DomainKeyId = baseApplicationUserToDomainKeyMap.DomainKeyId,
                ConnectionString = baseApplicationUserToDomainKeyMap.DomainKey.ConnectionString
            };

            _cache.Set(GetCacheKey(Id), result, DateTimeOffset.Now.AddYears(100));

            return result;
        }
    }
}
