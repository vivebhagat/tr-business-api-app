using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Update;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Setup;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;
using static Dapper.SqlMapper;

namespace PropertySolutionHub.Infrastructure.DataAccess
{
    public interface ILocalDbContext
    {
        DbSet<AdminRole> AdminRoles { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<AdminToRoleMap> AdminToRoleMaps { get; set; }
        DbSet<BusinessUserRole> BusinessUserRoles { get; set; }
        DbSet<BusinessUser> BusinessUsers { get; set; }
        DbSet<BusinessUserToRoleMap> BusinessUserToRoleMaps { get; set; }
        DbSet<CustomerRole> CustomerRoles { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerToRoleMap> CustomerToRoleMaps { get; set; }
        DbSet<LeaseAgreement> Lease { get; set; }
        DbSet<LeaseRequest> LeaseRequests { get; set; }
        DbSet<Property> Property { get; set; }
        DbSet<PropertyImage> PropertyImages { get; set; }
        DbSet<PropertyReview> PropertyReview { get; set; }

        DbSet<ContractRequest> ContractRequests { get; set; }
        DbSet<Contract> Contracts { get; set; }
        DbSet<Organization> Organizations { get; set; }

        DbSet<Community> Communities { get; set; }
        DbSet<CommunityToPropertyMap> CommunityToPropertyMaps { get; set; }
        DbSet<CommunityType> CommunityTypes { get; set; }
        DbSet<ConstructionStatus> ConstructionStatus { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void SetStateAsModified<TEntity>(TEntity @object) where TEntity : class;
        void SetStateAsAdded<TEntity>(TEntity @object) where TEntity : class;
    }
}