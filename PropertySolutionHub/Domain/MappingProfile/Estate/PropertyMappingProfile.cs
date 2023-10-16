using AutoMapper;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Domain.MappingProfile.Estate
{
    public class PropertyMappingProfile : Profile
    {
        public PropertyMappingProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>();
        }
    }
}