using AutoMapper;
using PropertySolutionHub.Api.Dto.Users;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Domain.MappingProfile.User
{
    public class BusinessUserMappingProfile : Profile
    {
        public BusinessUserMappingProfile()
        {
            CreateMap<BusinessUser, BusinessUserDto>();
            CreateMap<BusinessUserDto, BusinessUser>();
        }
    }
}