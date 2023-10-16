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
    public interface ICustomerRepository : IDynamicDbRepository
    {
        Task<int> CreateCustomer(Customer customer, string dataroute);
        Task<bool> DeleteCustomer(int customerId);
        Task<Customer> GetCustomerById(int customerId);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> UpdateCustomer(Customer customer);
        Task<RoleAuthResponse> Login(string username, string password);
        Task<RoleAuthResponse> RoleSelect(string role, string refreshToken);
        Task<bool> Logout(string userId);
        Task<List<CustomerToRoleMap>> GetCustomerRoleByUserId(string userId);
        Task<Customer> GetCustomerByUserId(string userId);

    }

    public class CustomerRepository : AbstractDynamicDbRepository, ICustomerRepository
    {
        IMemoryCache _cache;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<BaseApplicationUser> _userManager;
        IAuthDbContext authDb;
        ILocalDbContext db;

        public CustomerRepository(IAuthRepository authRepository, ILocalDbContext db, UserManager<BaseApplicationUser> userManager, IAuthDbContext authDb, IMemoryCache cache) : base(cache, authDb)
        {
            _authRepository = authRepository;
            this.db = new DbFactory<LocalDbContext>(GetConnectionString()).CreateDbContext();
            _userManager = userManager;
            this.authDb = authDb;
        }

        public void Validate(Customer customer)
        {
            ValidationHelper.CheckIsNull(customer);
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(customer.FirstName), "First name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(customer.LastName), "Last name is required.");
            ValidationHelper.CheckException(string.IsNullOrWhiteSpace(customer.UserName), "User name is required.");

            ValidationHelper.ValidateEmail(customer.Email);

            if (!string.IsNullOrWhiteSpace(customer.Password))
                ValidationHelper.ValidatePasswordFormat(customer.Password);
        }

        public async Task<RoleAuthResponse> Login(string username, string password)
        {
            try
            {
                BaseApplicationUser user = await ValidateUser(username, password);
                    
                if(user == null) return null;
                string accessToken = _authRepository.GenerateJwtToken(user, "Customer");
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

                    return new RoleAuthResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refToken.Id,
                        DataRoute = dataroute,
                        ExpireAt = expireat,
                        UserId = user.Id,
                        UserName = user.UserName,
                        RoleName = "Customer",
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

        private List<string> GetCustomerRoles(string userId)
        {
            try
            {
                var roles = db.CustomerToRoleMaps
                    .Where(m => m.Customer.UserId == userId && m.ArchiveDate == null)
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
                CustomerToRoleMap customerToRoleMap = db.CustomerToRoleMaps.Where(m => m.Customer.UserId == userId && m.Role.Name == role).FirstOrDefault();

                BaseApplicationUser user = new()
                {
                    UserName = userName,
                    Id = userId,
                    DataRoute = dataroute

                };

                if (customerToRoleMap == null)
                    return null;

                string accessToken = _authRepository.GenerateJwtToken(user, customerToRoleMap.Role.Id.ToString());

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
                        Role = customerToRoleMap.RoleId,
                        RoleName = customerToRoleMap.Role.Name
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

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> CreateCustomer(Customer customer, string dataroute)
        {
            try
            {
                Validate(customer);

                var applicationUser = new BaseApplicationUser
                {
                    UserName = customer.UserName,
                    Email = customer.Email,
                    Password = customer.Password,
                    DataRoute = dataroute,
                    BaseApplicationUserTypeId = 2,

                };

                string result = await _authRepository.RegisterUser(applicationUser);

                customer.UserId = result.ToString();
                customer.CreatedDate = DateTime.Now;

                db.Customers.Add(customer);
                db.SaveChanges();

                return customer.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer. " + ex.Message);
            }
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            try
            {
                Validate(customer);

                BaseApplicationUser commonApplicationUser = new BaseApplicationUser
                {
                    UserName = customer.UserName,
                    Email = customer.Email,
                    Password = customer.Password,
                };

                await _authRepository.UpdateUser(commonApplicationUser, customer.UserId);
                customer.ModifiedDate = DateTime.Now;
                db.SetStateAsModified(customer);
                db.SaveChanges();

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer. " + ex.Message);
            }
        }

        public async Task<bool> DeleteCustomer(int customerId)
        {
            try
            {
                Customer customer = db.Customers.Where(m => m.Id == customerId && m.ArchiveDate == null).FirstOrDefault() ?? throw new Exception("Customer details not found.");
                customer.ModifiedDate = DateTime.Now;
                customer.ArchiveDate = DateTime.Now;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting customer. " + ex.Message);
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await db.Customers.Where(m => m.ArchiveDate == null).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await db.Customers.Where(m => m.Id == customerId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }


        public async Task<Customer> GetCustomerByUserId(string userId)
        {
            return await db.Customers.Where(m => m.UserId == userId && m.ArchiveDate == null).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerToRoleMap>> GetCustomerRoleByUserId(string userId)
        {
            try
            {
                int customerId = db.Customers.Where(m => m.UserId == userId && m.ArchiveDate == null).Select(m => m.Id).FirstOrDefault();
                List<CustomerToRoleMap> roles = await db.CustomerToRoleMaps.Where(m => m.CustomerId == customerId && m.ArchiveDate == null).ToListAsync();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customer roles. " + ex.Message);
            }
        }
    }
}
