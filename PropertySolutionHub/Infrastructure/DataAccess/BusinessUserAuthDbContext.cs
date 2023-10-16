using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Infrastructure.DataAccess
{
    public interface IBusinessUserAuthDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
    }

    public class BusinessUserAuthDbContext : IdentityDbContext<ApplicationUser>, IBusinessUserAuthDbContext
    {
        public readonly string UserName = "businessuser@demo.com";
        public readonly string Password = "businessuser@123";
        public readonly string DataRoute = "BusinessUser";

        public BusinessUserAuthDbContext(DbContextOptions<BusinessUserAuthDbContext> options, IConfiguration configuration) : base(options)
        {
        }

        protected BusinessUserAuthDbContext()
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("businessuser");

            ApplicationUser businessuserUser = new()
            {
                UserName = UserName,
                Email = UserName,
                Password = Password,
                NormalizedUserName = UserName.ToUpper().Normalize(),
                NormalizedEmail = UserName.ToUpper().Normalize(),
                DataRoute = DataRoute
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            businessuserUser.PasswordHash = passwordHasher.HashPassword(businessuserUser, Password);
            builder.Entity<ApplicationUser>().HasData(businessuserUser);
        }
    }
}