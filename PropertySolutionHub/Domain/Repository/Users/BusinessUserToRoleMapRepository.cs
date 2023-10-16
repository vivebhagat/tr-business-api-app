using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IBusinessUserToRoleMapRepository : IDynamicDbRepository
    {
        void Validate(BusinessUserToRoleMap role);
        Task<List<BusinessUserToRoleMap>> GetBusinessUserRoleByUserId(string userId);
        Task<List<BusinessUserRole>> GetAllBusinessUserRoles();
        Task<int> CreateBusinessuserToRoleMap(BusinessUserToRoleMap @object);
        Task<BusinessUserToRoleMap> UpdateBusinessuserToRoleMap(BusinessUserToRoleMap @object);
        Task<List<BusinessUserToRoleMap>> GetBusinessUserRoleListById(int Id);
    }

    public class BusinessUserToRoleMapRepository : AbstractDynamicDbRepository, IBusinessUserToRoleMapRepository
    {
        IMemoryCache _cache;
        IAuthDbContext _authDb;
        ILocalDbContext db;

        public BusinessUserToRoleMapRepository(ILocalDbContext db, DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb) : base(cache, authDb)
        {
            _cache = cache;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _authDb = authDb;
        }

        public void Validate(BusinessUserToRoleMap @object)
        {
            ValidationHelper.CheckIsNull(@object);
            ValidationHelper.CheckException(@object.BusinessUserId == 0, "Member is required.");
            ValidationHelper.CheckException(@object.RoleId == 0, "Role is required.");
        }

        public async Task<int> CreateBusinessuserToRoleMap(BusinessUserToRoleMap @object)
        {
            try
            {
                Validate(@object);
                @object.CreatedDate = DateTime.Now;
                db.BusinessUserToRoleMaps.Add(@object);
                await db.SaveChangesAsync();
                return @object.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding member. " + ex.Message);
            }
        }


        public async Task<BusinessUserToRoleMap> UpdateBusinessuserToRoleMap(BusinessUserToRoleMap @object)
        {
            try
            {
                Validate(@object);
                @object.CreatedDate = DateTime.Now;
                db.SetStateAsModified(@object);
                await db.SaveChangesAsync();
                return @object;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating member. " + ex.Message);
            }
        }

        public async Task<List<BusinessUserToRoleMap>> GetBusinessUserRoleListById(int Id)
        {
            try
            {
                List<BusinessUserToRoleMap> roles = await db.BusinessUserToRoleMaps.Where(m => m.BusinessUserId == Id && m.ArchiveDate == null).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving roles. " + ex.Message);
            }
        }

        public async Task<List<BusinessUserToRoleMap>> GetBusinessUserRoleByUserId(string userId)
        {
            try
            {
                int businessUserId = db.BusinessUsers.Where(m => m.UserId == userId && m.ArchiveDate == null).Select(m => m.Id).FirstOrDefault();
                List<BusinessUserToRoleMap> roles = await db.BusinessUserToRoleMaps.Where(m => m.BusinessUserId == businessUserId && m.ArchiveDate == null).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving businessUser roles. " + ex.Message);
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
    }
}
