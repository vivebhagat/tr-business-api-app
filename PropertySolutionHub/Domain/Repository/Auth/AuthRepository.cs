using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Infrastructure.DataAccess;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PropertySolutionHub.Domain.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<string> RegisterUser(BaseApplicationUser applicationUser);
        Task<bool> UpdateUser(BaseApplicationUser applicationUser, string userId);
        Task<BaseApplicationUser> ValidateUser(string userName, string password);
        string GenerateJwtToken(BaseApplicationUser user, string authRole);
        Task<bool> AddRefreshToken(RefreshToken refreshToken);
        Task<bool> RemoveRefreshToken(string userId);
    }

    public class AuthRepository : IAuthRepository
    {
        IAuthDbContext _authDb;
        private readonly UserManager<BaseApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<BaseApplicationUser> userManager, IConfiguration configuration, IAuthDbContext authDb)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authDb = authDb;
        }

        public async Task<string> RegisterUser(BaseApplicationUser applicationUser)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(applicationUser.UserName);

                if (existingUser != null)
                    throw new Exception("User name already in use.");

                var result = await _userManager.CreateAsync(applicationUser, applicationUser.Password);

                if (result.Succeeded)
                {
                    var newUser = await _userManager.FindByNameAsync(applicationUser.UserName);
                    return newUser.Id;
                }
                else
                    throw new Exception("Error while creating user.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateUser(BaseApplicationUser applicationUser, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    throw new Exception("Failed to update user details. User not found.");

                if (applicationUser.Password != null)
                {
                    var passwordChangeResult = await _userManager.RemovePasswordAsync(user);

                    if (!passwordChangeResult.Succeeded)
                        throw new Exception("Error while changing password.");

                    var passwordChanegd = await _userManager.AddPasswordAsync(user, applicationUser.Password);

                    if (!passwordChanegd.Succeeded)
                        throw new Exception("Error while changing password.");

                }

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                    return true;

                throw new Exception("Error while updating user.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddRefreshToken(RefreshToken refreshToken)
        {
            var result = await RemoveRefreshToken(refreshToken.UserId);

            if (!result)
                return false;

            _authDb.RefreshTokens.Add(refreshToken);

            return await _authDb.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string userId)
        {
            var existingRefreshToken = _authDb.RefreshTokens.Where(m => m.UserId == userId).FirstOrDefault();

            if (existingRefreshToken == null)
                return true;

            _authDb.RefreshTokens.Remove(existingRefreshToken);
            return await _authDb.SaveChangesAsync() > 0;
        }


        public async Task<BaseApplicationUser> ValidateUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
                return user;

            return null;
        }


        public string GenerateJwtToken(BaseApplicationUser user, string authRole)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JWT");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("DataRoute", user.DataRoute)
                };

                if (!string.IsNullOrEmpty(authRole))
                    claims.Add(new Claim("authRole", authRole));

                DateTime specificLocalTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["ValidIssuer"],
                    audience: jwtSettings["ValidAudience"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);

            }
            catch (Exception e)
            {
                throw new Exception("Failed to generate token. " + e.Message);
            }
        }
    }
}