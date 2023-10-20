using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Setup;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using static Dapper.SqlMapper;

namespace PropertySolutionHub.Infrastructure.DataAccess
{
    public class LocalDbContext : DbContext, ILocalDbContext
    {
        private static readonly bool isDataSeeded = false;

        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {

        }

        public LocalDbContext() : base()
        {

        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRole> CustomerRoles { get; set; }
        public DbSet<CustomerToRoleMap> CustomerToRoleMaps { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<AdminToRoleMap> AdminToRoleMaps { get; set; }

        public DbSet<BusinessUser> BusinessUsers { get; set; }
        public DbSet<BusinessUserRole> BusinessUserRoles { get; set; }
        public DbSet<BusinessUserToRoleMap> BusinessUserToRoleMaps { get; set; }

        public DbSet<LeaseRequest> LeaseRequests { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<LeaseAgreement> Lease { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyReview> PropertyReview { get; set; }

        public DbSet<ContractRequest> ContractRequests { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Organization> Organizations { get; set; }




        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }
        public void SetStateAsAdded<TEntity>(TEntity @object) where TEntity : class
        {
            base.Entry(@object).State = EntityState.Added;
        }

        public void SetStateAsModified<TEntity>(TEntity @object) where TEntity : class
        {
            base.Entry(@object).State = EntityState.Modified;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            base.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            base.Update(entity);
        }


        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            base.Remove(entity);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("data");

           /* if (!isDataSeeded)
            {
                //Default admin
                var systemAdminRole = new AdminRole { Id = 1, Name = "System Administrator", CreatedDate = DateTime.Now };
                builder.Entity<AdminRole>().HasData(systemAdminRole);

                var adminDetails = new Admin { Id = 1, Name = "admin@demo.com", CreatedDate = DateTime.Now };
                builder.Entity<Admin>().HasData(adminDetails);

                var adminToRoleMapping = new AdminToRoleMap { Id = 1, AdminId = adminDetails.Id, RoleId = systemAdminRole.Id, CreatedDate = DateTime.Now };
                builder.Entity<AdminToRoleMap>().HasData(adminToRoleMapping);
            }*/
        }
    }
}