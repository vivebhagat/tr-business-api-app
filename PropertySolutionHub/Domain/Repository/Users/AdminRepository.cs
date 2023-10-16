using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Auth;
using PropertySolutionHub.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IAdminRepository
    {
        Task<int> CreateAdmin(Admin admin, string dataroute);
        Task<bool> DeleteAdmin(int adminId);
        Task<Admin> GetAdminById(int adminId);
        Task<List<Admin>> GetAllAdmins();
        Task<Admin> UpdateAdmin(Admin admin);
        Task<AuthResponse> Login(string username, string password);
        Task<RoleAuthResponse> RoleSelect(string role, string refreshToken);
        Task<bool> Logout(string userId);
        Task<List<AdminToRoleMap>> GetAdminRoleByUserId(string userId);

    }

    public class AdminRepository : IAdminRepository
    {
        IAuthDbContext authDb;
        private readonly IAuthRepository _authRepository;
        ILocalDbContext db;

        public AdminRepository(IAuthRepository authRepository, ILocalDbContext db, IAuthDbContext authDb)
        {
            _authRepository = authRepository;
            this.db = db;
            this.authDb = authDb;
        }

        public void Validate(Admin admin)
        {
            ValidationHelper.CheckIsNull(admin);
            ValidationHelper.ValidateEmail(admin.Email);

            if(!string.IsNullOrWhiteSpace(admin.Password))
                ValidationHelper.ValidatePasswordFormat(admin.Password);
        }

        public async Task<AuthResponse> Login(string username, string password)
        {
            try
            {
                BaseApplicationUser user = await _authRepository.ValidateUser(username, password) ?? throw new Exception("Invalid login credentials. Please check username and password and try again.");
                string accessToken = _authRepository.GenerateJwtToken(user, string.Empty);
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                string dataroute = jwt.Claims.First(c => c.Type == "DataRoute").Value;
                var expireat = jwt.ValidTo;

                RefreshToken refToken = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = accessToken,
                    UserName = user.UserName,
                    UserId = user.Id,
                    IssuedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddHours(2)
                };

                bool result  = await _authRepository.AddRefreshToken(refToken);

                if (result)
                {
                    return new AuthResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refToken.Id,
                        DataRoute = dataroute,
                        ExpireAt = expireat,
                        UserId = user.Id,
                        UserName = user.UserName,
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<string> GetAdminRoles(string userId)
        {
            try
            {
                var roles = db.AdminToRoleMaps
                    .Where(m => m.Admin.UserId == userId && m.ArchiveDate == null)
                    .Select(m => m.Role.Name)
                    .ToList();

                return roles;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<RoleAuthResponse> RoleSelect(string role, string refreshToken)
        {
            try
            {
                RefreshToken existingRefreshToken = authDb.RefreshTokens.Where(m => m.Id == refreshToken).FirstOrDefault();

                if (existingRefreshToken == null)
                    return null;

                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(existingRefreshToken.Token);
                string dataroute = jwt.Claims.First(c => c.Type == "DataRoute").Value;
                DateTime expireat = jwt.ValidTo;
                string userId = jwt.Claims.First(c => c.Type == "sub").Value;
                string userName = jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;

                AdminToRoleMap adminToRoleMap = db.AdminToRoleMaps.Where(m => m.Admin.UserId == userId && m.Role.Name == role).FirstOrDefault();

                BaseApplicationUser user = new()
                {
                    UserName = userName,
                    Id = userId,
                    DataRoute = dataroute

                };

                if (adminToRoleMap == null)
                    throw new UnauthorizedAccessException("Unauthorize access. Role is not authorized.");

                string accessToken = _authRepository.GenerateJwtToken(user, adminToRoleMap.Role.Id.ToString());

                RefreshToken refToken = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = accessToken,
                    UserName = user.UserName,
                    UserId = user.Id,
                    IssuedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddHours(2)
                };

                bool result = await _authRepository.AddRefreshToken(refToken);

                if (result) {
                    return new RoleAuthResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refToken.Id,
                        DataRoute = dataroute,
                        ExpireAt = expireat,
                        UserId = user.Id,
                        UserName = user.UserName,
                        Role = adminToRoleMap.RoleId,
                        RoleName = adminToRoleMap.Role.Name
                    };
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }

        public async Task<bool> Logout(string userId)
        {
            try
            {
                return await _authRepository.RemoveRefreshToken(userId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> CreateAdmin(Admin admin, string dataroute)
        {
            try
            {
                Validate(admin);

                var applicationUser = new BaseApplicationUser
                {
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Password = admin.Password,
                    DataRoute = dataroute,
                };

                string result = await _authRepository.RegisterUser(applicationUser);

                admin.UserId = result.ToString();
                admin.CreatedDate = DateTime.Now;

                db.Admins.Add(admin);
                db.SaveChanges();

                return admin.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding admin. " + ex.Message);
            }
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            try
            {
                Validate(admin);

                BaseApplicationUser commonApplicationUser = new BaseApplicationUser
                {
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Password = admin.Password,
                };

                await _authRepository.UpdateUser(commonApplicationUser, admin.UserId);
                admin.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(admin);
                db.SaveChanges();

                return admin;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating admin. " + ex.Message);
            }
        }

        public async Task<bool> DeleteAdmin(int adminId)
        {
            try
            {
                Admin admin = db.Admins.Where(m => m.Id == adminId && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("Admin details not found.");
                admin.ModifiedDate = DateTime.Now;
                admin.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting admin. " + ex.Message);
            }
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            return await db.Admins.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Admin> GetAdminById(int adminId)
        {
            return await db.Admins.Where(m => m.Id == adminId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<List<AdminToRoleMap>> GetAdminRoleByUserId(string userId)
        {
            try
            {
                int adminId = db.Admins.Where(m => m.UserId == userId && m.ArchiveDate == null).Select(m => m.Id).FirstOrDefault();
                return await db.AdminToRoleMaps.Where(m => m.AdminId == adminId && m.ArchiveDate == null).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving admin roles. " + ex.Message);
            }
        }
    }
}
