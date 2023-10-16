using Castle.Core.Resource;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Auth;
using PropertySolutionHub.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace PropertySolutionHub.Domain.Repository.Users
{
    public interface IBusinessUserRepository : IDynamicDbRepository
    {
        Task<int> CreateBusinessUser(BusinessUser businessUser);
        Task<bool> DeleteBusinessUser(int businessUserId);
        Task<BusinessUser> GetBusinessUserById(int businessUserId);
        Task<List<BusinessUser>> GetAllBusinessUsers();
        Task<BusinessUser> UpdateBusinessUser(BusinessUser businessUser);
        Task<AuthResponse> Login(string username, string password);
        Task<RoleAuthResponse> RoleSelect(string role, string refreshToken);
        Task<bool> Logout(string userId);
        Task<BusinessUser> GetBusinessUserByUserId(string userId);

    }

    public class BusinessUserRepository : AbstractDynamicDbRepository, IBusinessUserRepository
    {
        IMemoryCache _cache;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<BaseApplicationUser> _userManager;
        IAuthDbContext authDb;
        ILocalDbContext db;
        IHttpHelper _httpHelper;

        public BusinessUserRepository(DapperContext dapperContext, IMemoryCache cache, IAuthDbContext authDb, IAuthRepository authRepository, UserManager<BaseApplicationUser> userManager, IHttpHelper httpHelper) : base(cache, authDb)
        {
            _cache = cache;
            _authRepository = authRepository;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _userManager = userManager;
            this.authDb = authDb;
        }

        public void Validate(BusinessUser businessUser)
        {
            ValidationHelper.CheckIsNull(businessUser);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(businessUser.FirstName), "First name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(businessUser.LastName), "Last name is required.");
            ValidationHelper.ValidateEmail(businessUser.Email);

            if (!string.IsNullOrWhiteSpace(businessUser.Password))
                ValidationHelper.ValidatePasswordFormat(businessUser.Password);
        }

        public async Task<AuthResponse> Login(string username, string password)
        {
            try
            {
                BaseApplicationUser user = await ValidateUser(username, password);

                if (user == null) return null;

                if (user.BaseApplicationUserTypeId != 0 && user.BaseApplicationUserType.Name.ToLower() != "businessuser") return null;

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

                bool result = await _authRepository.AddRefreshToken(refToken);

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
                return null;
            }
        }

        private async Task<BaseApplicationUser> ValidateUser(string userName, string password)
        {

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            if (await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }

        private List<string> GetBusinessUserRoles(string userId)
        {
            try
            {
                var roles = db.BusinessUserToRoleMaps
                    .Where(m => m.BusinessUser.UserId == userId && m.ArchiveDate == null)
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

                object queryParams = new { UserId = userId, RoleName = role };
                BusinessUserToRoleMap businessUserToRoleMap = db.BusinessUserToRoleMaps.Where(m => m.BusinessUser.UserId == userId && m.Role.Name == role).FirstOrDefault();

                BaseApplicationUser user = new()
                {
                    UserName = userName,
                    Id = userId,
                    DataRoute = dataroute

                };

                if (businessUserToRoleMap == null)
                    return null;

                string accessToken = _authRepository.GenerateJwtToken(user, businessUserToRoleMap.Role.Id.ToString());

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

                if (result)
                {
                    return new RoleAuthResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refToken.Id,
                        DataRoute = dataroute,
                        ExpireAt = expireat,
                        UserId = user.Id,
                        UserName = user.UserName,
                        Role = businessUserToRoleMap.RoleId,
                        RoleName = businessUserToRoleMap.Role.Name
                    };
                }

                return null;
            }
            catch (Exception e)
            {
                return null;

            }
        }



        public async Task<bool> Logout(string userId)
        {
            try
            {
                return await _authRepository.RemoveRefreshToken(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> CreateBusinessUser(BusinessUser businessUser)
        {
            try
            {
                Validate(businessUser);

                var applicationUser = new BaseApplicationUser
                {
                    UserName = businessUser.UserName,
                    Email = businessUser.Email,
                    Password = businessUser.Password,
                    DataRoute = "BusinessUser",
                    BaseApplicationUserTypeId = 1,
                };

                string result = await _authRepository.RegisterUser(applicationUser);

                businessUser.UserId = result.ToString();
                businessUser.CreatedDate = DateTime.Now;

                db.BusinessUsers.Add(businessUser);
                db.SaveChanges();

                return businessUser.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding businessUser. " + ex.Message);
            }
        }

        public async Task<BusinessUser> UpdateBusinessUser(BusinessUser businessUser)
        {
            try
            {
                Validate(businessUser);

                BaseApplicationUser commonApplicationUser = new BaseApplicationUser
                {
                    UserName = businessUser.UserName,
                    Email = businessUser.Email,
                    Password = businessUser.Password,
                };

                await _authRepository.UpdateUser(commonApplicationUser, businessUser.UserId);
                businessUser.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(businessUser);
                db.SaveChanges();

                return businessUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating businessUser. " + ex.Message);
            }
        }

        public async Task<bool> DeleteBusinessUser(int businessUserId)
        {
            try
            {
                BusinessUser businessUser = db.BusinessUsers.Where(m => m.Id == businessUserId && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("BusinessUser details not found.");
                businessUser.ModifiedDate = DateTime.Now;
                businessUser.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting businessUser. " + ex.Message);
            }
        }

        public async Task<List<BusinessUser>> GetAllBusinessUsers()
        {
            return await db.BusinessUsers.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<BusinessUser> GetBusinessUserById(int businessUserId)
        {
            return await db.BusinessUsers.Where(m => m.Id == businessUserId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<BusinessUser> GetBusinessUserByUserId(string userId)
        {
            return await db.BusinessUsers.Where(m => m.UserId == userId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }
    }
}
