using AutoMapper;
using PropertySolutionHub.Domain.MappingProfile.Estate;
using PropertySolutionHub.Domain.MappingProfile.User;

namespace PropertySolutionHub.Domain.MappingProfile
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PropertyMappingProfile>();
                cfg.AddProfile<ContractRequestMappingProfile>();
                cfg.AddProfile<BusinessUserMappingProfile>();
                cfg.AddProfile<PropertyImageMappingProfile>();
                cfg.AddProfile<BusinessUserToRoleMapMappingProfile>();
                cfg.AddProfile<BusinessUserRoleMappingProfile>();

                cfg.AddProfile<CommunityMappingProfile>();
                cfg.AddProfile<CommunityToPropertyMapMappingProfile>();


            });
            return config;
        }
    }
}
