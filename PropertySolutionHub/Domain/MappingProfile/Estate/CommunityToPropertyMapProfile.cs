using AutoMapper;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Domain.MappingProfile.Estate
{
    public class CommunityToPropertyMapMappingProfile : Profile
    {
        public CommunityToPropertyMapMappingProfile()
        {
            CreateMap<CommunityToPropertyMap, CommunityToPropertyMapDto>()
                .ForMember(dest => dest.PropertyName, opt => opt.MapFrom(src => src.Property.Name))
                .ForMember(dest => dest.PropertyPrice, opt => opt.MapFrom(src => src.Property.Price))
                .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.Property.UnitType))
                .ForMember(dest => dest.PropertyStatus, opt => opt.MapFrom(src => src.Property.Status));
            CreateMap<CommunityToPropertyMapDto, CommunityToPropertyMap>();
        }
    }
}