using ContractRequestSolutionHub.Domain.Repository.Estate;
using OrganizationSolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Auth;
using PropertySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Repository.Users;
using PropertySolutionHub.Domain.Service.Email;

namespace PropertySolutionHub.Infrastructure.DataAccess
{
    public static class AppServiceRegistry
    {
        public static void LoadServices(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAzureEmailService, AzureEmailService>();
            services.AddScoped<IStorageHelper, StorageHelper>();
            services.AddScoped<IHttpHelper, HttpHelper>();

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBusinessUserToRoleMapRepository, BusinessUserToRoleMapRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBusinessUserRepository, BusinessUserRepository>();
            services.AddScoped<ILeaseAgreementRepository, LeaseAgreementRepository>();
            services.AddScoped<ILeaseRequestRepository, LeaseRequestRepository>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<IPropertyReviewRepository, PropertyReviewRepository>();
            services.AddScoped<IAdminToRoleMapRepository, AdminToRoleMapRepository>();
            services.AddScoped<IDomainKeyRepository, DomainKeyRepository>();
            services.AddScoped<IBaseApplicationUserToDomainKeyMapRepository, BaseApplicationUserToDomainKeyMapRepository>();
            services.AddScoped<IContractRequestRepository, ContractRequestRepository>();
            services.AddScoped<IBusinessUserRoleRepository, BusinessUserRoleRepository>();

        }
    }
}
