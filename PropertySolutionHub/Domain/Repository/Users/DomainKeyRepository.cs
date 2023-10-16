using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Auth;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IDomainKeyRepository
    {
        Task<int> CreateDomainKey(DomainKey domainKey);
        Task<bool> DeleteDomainKey(int domainKeyId);
        Task<DomainKey> GetDomainKeyById(int domainKeyId);
        Task<List<DomainKey>> GetAllDomainKeys();
        Task<DomainKey> UpdateDomainKey(DomainKey domainKey);
    }

    public class DomainKeyRepository : IDomainKeyRepository
    {
        IAuthDbContext db;

        public DomainKeyRepository(IAuthDbContext db)
        {
            this.db = db;
        }

        public void Validate(DomainKey domainKey)
        {
            ValidationHelper.CheckIsNull(domainKey);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(domainKey.Name), "Name is required.");
            ValidationHelper.CheckException(domainKey.Value <= 0, "Invalid domain key value.");
        }

        public async Task<int> CreateDomainKey(DomainKey domainKey)
        {
            try
            {
                Validate(domainKey);
                domainKey.CreatedDate = DateTime.Now;
                db.DomainKeys.Add(domainKey);
                await db.SaveChangesAsync();

                return domainKey.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding domainKey. " + ex.Message);
            }
        }

        public async Task<DomainKey> UpdateDomainKey(DomainKey domainKey)
        {
            try
            {
                Validate(domainKey);
                domainKey.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(domainKey);
                await db.SaveChangesAsync();
                return domainKey;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating domainKey. " + ex.Message);
            }
        }

        public async Task<bool> DeleteDomainKey(int domainKeyId)
        {
            try
            {
                DomainKey domainKey = db.DomainKeys.Where(m => m.Id == domainKeyId && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("DomainKey details not found.");
                domainKey.ModifiedDate = DateTime.Now;
                domainKey.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting domainKey. " + ex.Message);
            }
        }

        public async Task<List<DomainKey>> GetAllDomainKeys()
        {
            return await db.DomainKeys.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<DomainKey> GetDomainKeyById(int domainKeyId)
        {
            return await db.DomainKeys.Where(m => m.Id == domainKeyId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
