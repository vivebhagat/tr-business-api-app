using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Estate
{
    public interface ICommunityTypeRepository : IDynamicDbRepository
    {
        Task<int> CreateCommunityType(CommunityType community);
        Task<bool> DeleteCommunityType(int Id);
        Task<CommunityType> GetCommunityTypeById(int Id);
        Task<List<CommunityType>> GetAllCommunityTypes();
        Task<CommunityType> UpdateCommunityType(CommunityType community);
    }

    public class CommunityTypeRepository : AbstractDynamicDbRepository, ICommunityTypeRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        private readonly string DomainKey = string.Empty;

        public CommunityTypeRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _httpHelper = httpHelper;
            DomainKey = GetDomainKey();
        }

        public void Validate(CommunityType community)
        {
            ValidationHelper.CheckIsNull(community);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(community.Name), "Name is required.");
        }

        public async Task<int> CreateCommunityType(CommunityType community)
        {

            try
            {
                Validate(community);

                if (db.CommunityTypes.Any(m => m.Name == community.Name && m.ArchiveDate == null))
                    throw new Exception("A community with the same name already exists.");

                community.CreatedDate = DateTime.Now;
                db.CommunityTypes.Add(community);
                await db.SaveChangesAsync();
                return community.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding community. " + ex.Message);
            }
        }

        public async Task<CommunityType> UpdateCommunityType(CommunityType community)
        {
            try
            {
                Validate(community);

                if (db.CommunityTypes.Any(m => m.Name == community.Name && m.ArchiveDate == null))
                    throw new Exception("A community with the same name already exists.");

                community.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(community);
                await db.SaveChangesAsync();
                return community;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating community. " + ex.Message);
            }
        }

        public async Task<bool> DeleteCommunityType(int Id)
        {
            try
            {
                CommunityType community = db.CommunityTypes.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (community == null)
                    throw new Exception("CommunityType does not exist.");

                community.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting community. " + ex.Message);
            }
        }

        public async Task<List<CommunityType>> GetAllCommunityTypes()
        {
            return await db.CommunityTypes.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<CommunityType> GetCommunityTypeById(int Id)
        {
            return await db.CommunityTypes.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
