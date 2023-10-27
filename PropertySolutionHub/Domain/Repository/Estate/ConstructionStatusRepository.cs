using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace ConstructionStatusSolutionHub.Domain.Repository.Estate
{
    public interface IConstructionStatusRepository : IDynamicDbRepository
    {
        Task<int> CreateConstructionStatus(ConstructionStatus constructionStatus);
        Task<bool> DeleteConstructionStatus(int Id);
        Task<ConstructionStatus> GetConstructionStatusById(int Id);
        Task<List<ConstructionStatus>> GetAllConstructionStatuss();
        Task<ConstructionStatus> UpdateConstructionStatus(ConstructionStatus constructionStatus);
    }

    public class ConstructionStatusRepository : AbstractDynamicDbRepository, IConstructionStatusRepository
    {
        IMemoryCache _cache;
        ILocalDbContext db;
        IAuthDbContext _authDb;
        IHttpHelper _httpHelper;
        private readonly string DomainKey = string.Empty;

        public ConstructionStatusRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IHttpHelper httpHelper) : base(cache, authDb)
        {
            _cache = cache;
            _authDb = authDb;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _httpHelper = httpHelper;
            DomainKey = GetDomainKey();
        }

        public void Validate(ConstructionStatus constructionStatus)
        {
            ValidationHelper.CheckIsNull(constructionStatus);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(constructionStatus.Name), "Name is required.");
        }

        public async Task<int> CreateConstructionStatus(ConstructionStatus constructionStatus)
        {

            try
            {
                Validate(constructionStatus);

                if (db.ConstructionStatus.Any(m => m.Name == constructionStatus.Name && m.ArchiveDate == null))
                    throw new Exception("A Status with the same name already exists.");

                constructionStatus.CreatedDate = DateTime.Now;
                db.ConstructionStatus.Add(constructionStatus);
                await db.SaveChangesAsync();
                return constructionStatus.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Status. " + ex.Message);
            }
        }

        public async Task<ConstructionStatus> UpdateConstructionStatus(ConstructionStatus constructionStatus)
        {
            try
            {
                Validate(constructionStatus);
                constructionStatus.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(constructionStatus);
                await db.SaveChangesAsync();
                return constructionStatus;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Status. " + ex.Message);
            }
        }

        public async Task<bool> DeleteConstructionStatus(int Id)
        {
            try
            {
                ConstructionStatus constructionStatus = db.ConstructionStatus.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault();

                if (constructionStatus == null)
                    throw new Exception("Status does not exist.");

                constructionStatus.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Status. " + ex.Message);
            }
        }

        public async Task<List<ConstructionStatus>> GetAllConstructionStatuss()
        {
            return await db.ConstructionStatus.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<ConstructionStatus> GetConstructionStatusById(int Id)
        {
            return await db.ConstructionStatus.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
