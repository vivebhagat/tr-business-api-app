using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Infrastructure.DataAccess
{
    public interface ICustomerAuthDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
    }

    public class CustomerAuthDbContext : IdentityDbContext<ApplicationUser>, ICustomerAuthDbContext
    {
        public readonly string UserName = "customer@demo.com";
        public readonly string Password = "customer@123";
        public readonly string DataRoute = "Customer";

        public CustomerAuthDbContext(DbContextOptions<CustomerAuthDbContext> options, IConfiguration configuration) : base(options)
        {
        }

        protected CustomerAuthDbContext()
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("customer");

            ApplicationUser customerUser = new()
            {
                UserName = UserName,
                Email = UserName,
                Password = Password,
                NormalizedUserName = UserName.ToUpper().Normalize(),
                NormalizedEmail = UserName.ToUpper().Normalize(),
                DataRoute = DataRoute
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            customerUser.PasswordHash = passwordHasher.HashPassword(customerUser, Password);
            builder.Entity<ApplicationUser>().HasData(customerUser);
        }
    }
}