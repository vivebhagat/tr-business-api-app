using AutoMapper;
using PropertySolutionHub.Api.Dto.Users;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Domain.MappingProfile.User
{
    public class BusinessUserToRoleMapMappingProfile : Profile
    {
        public BusinessUserToRoleMapMappingProfile()
        {
            CreateMap<BusinessUserToRoleMap, BusinessUserToRoleMapDto>();
            CreateMap<BusinessUserToRoleMapDto, BusinessUserToRoleMap>();
        }
    }
}