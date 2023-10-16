using AutoMapper;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Domain.MappingProfile.Estate
{
    public class PropertyImageMappingProfile : Profile
    {
        public PropertyImageMappingProfile()
        {
            CreateMap<PropertyImageDto, PropertyImage>();
            CreateMap<PropertyImage, PropertyImageDto>();
        }
    }
}