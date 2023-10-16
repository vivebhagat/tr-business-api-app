using Dapper;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Infrastructure.DataAccess;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IAdminToRoleMapRepository
    {
        Task<int> AddAdminToRoleMap(AdminToRoleMap role);
        void Validate(AdminToRoleMap role);
    }

    public class AdminToRoleMapRepository : IAdminToRoleMapRepository
    {
        ILocalDbContext db;
        private readonly DapperContext _dapperContext;

        public AdminToRoleMapRepository(ILocalDbContext db, DapperContext dapperContext)
        {
            this.db = db;
            _dapperContext = dapperContext;
        }

        public void Validate(AdminToRoleMap role)
        {
            ValidationHelper.CheckIsNull(role);
            ValidationHelper.CheckException(role.RoleId == 0, "Role is required.");
        }

        public async Task<int> AddAdminToRoleMap(AdminToRoleMap role)
        {
            Validate(role);

            string insertQuery = "INSERT INTO data.AdminToRoleMaps (AdminId, RoleId, CreatedDate) VALUES (@AdminId, @RoleId, @CreatedDate );";
            using var connection = _dapperContext.CreateConnection();
            return await connection.ExecuteAsync(insertQuery, role);

        }
    }
}
