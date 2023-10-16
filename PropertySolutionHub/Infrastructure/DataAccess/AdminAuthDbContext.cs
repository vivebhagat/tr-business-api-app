using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Infrastructure.DataAccess
{
    public interface IAdminAuthDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
    }

    public class AdminAuthDbContext : IdentityDbContext<ApplicationUser>, IAdminAuthDbContext
    {
        public readonly string UserName = "admin@demo.com";
        public readonly string Password = "admin@123";
        public readonly string DataRoute = "Admin";

        public AdminAuthDbContext(DbContextOptions<AdminAuthDbContext> options, IConfiguration configuration) : base(options)
        {
        }

        protected AdminAuthDbContext()
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("admin");

            ApplicationUser adminUser = new()
            {
                UserName = UserName,
                Email = UserName,
                Password = Password,
                NormalizedUserName = UserName.ToUpper().Normalize(),
                NormalizedEmail = UserName.ToUpper().Normalize(),
                DataRoute = DataRoute
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, Password);
            builder.Entity<ApplicationUser>().HasData(adminUser);
        }
    }
}