using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IBusinessUserRoleRepository : IDynamicDbRepository
    {
        void Validate(BusinessUserRole role);
        Task<List<BusinessUserRole>> GetAllBusinessUserRoles();
        Task<int> CreateBusinessUserRole(BusinessUserRole @object);
        Task<BusinessUserRole> UpdateBusinessUserRole(BusinessUserRole @object);
        Task<bool> DeleteBusinessUserRole(int Id);
        Task<BusinessUserRole> GetBusinessuserRoleById(int Id);
    }

    public class BusinessUserRoleRepository : AbstractDynamicDbRepository, IBusinessUserRoleRepository
    {
        IMemoryCache _cache;
        IAuthDbContext _authDb;
        ILocalDbContext db;

        public BusinessUserRoleRepository(ILocalDbContext db, DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb) : base(cache, authDb)
        {
            _cache = cache;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _authDb = authDb;
        }

        public void Validate(BusinessUserRole @object)
        {
            ValidationHelper.CheckIsNull(@object);
            ValidationHelper.CheckException(string.IsNullOrEmpty(@object.Name), "Name is required.");
        }

        public async Task<int> CreateBusinessUserRole(BusinessUserRole @object)
        {
            try
            {
                Validate(@object);

                if (db.BusinessUserRoles.Any(m => m.Name == @object.Name && m.ArchiveDate == null))
                {
                    throw new Exception("A Roole with the same name already exists.");
                }

                @object.CreatedDate = DateTime.Now;
                db.BusinessUserRoles.Add(@object);
                await db.SaveChangesAsync();
                return @object.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding role. " + ex.Message);
            }
        }


        public async Task<BusinessUserRole> UpdateBusinessUserRole(BusinessUserRole @object)
        {
            try
            {
                Validate(@object);

                if (db.BusinessUserRoles.Any(m => m.Name == @object.Name && m.ArchiveDate == null))
                {
                    throw new Exception("A Roole with the same name already exists.");
                }

                @object.CreatedDate = DateTime.Now;
                db.SetStateAsModified(@object);
                await db.SaveChangesAsync();
                return @object;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating role. " + ex.Message);
            }
        }

        public async Task<bool> DeleteBusinessUserRole(int Id)
        {
            try
            {
                BusinessUserRole businessUserRole = db.BusinessUserRoles.Where(m => m.Id == Id && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("Role details not found.");
                businessUserRole.ModifiedDate = DateTime.Now;
                businessUserRole.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting businessUser. " + ex.Message);
            }
        }

        public async Task<List<BusinessUserRole>> GetAllBusinessUserRoles()
        {
            try
            {
                return await db.BusinessUserRoles.Where(m => m.ArchiveDate == null).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving roles. " + ex.Message);
            }
        }

        public async Task<BusinessUserRole> GetBusinessuserRoleById(int Id)
        {
            try
            {
                return await db.BusinessUserRoles.Where(m => m.Id ==Id && m.ArchiveDate == null).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving role. " + ex.Message);
            }
        }
    }
}
