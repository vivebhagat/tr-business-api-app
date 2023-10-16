using AutoMapper;
using PropertySolutionHub.Api.Dto.Users;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Domain.MappingProfile.User
{
    public class BusinessUserRoleMappingProfile : Profile
    {
        public BusinessUserRoleMappingProfile()
        {
            CreateMap<BusinessUserRole, BusinessUserRoleDto>();
            CreateMap<BusinessUserRoleDto, BusinessUserRole>();
        }
    }
}