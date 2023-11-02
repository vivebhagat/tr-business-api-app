using AutoMapper;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Domain.MappingProfile.Estate
{
    public class CommunityMappingProfile : Profile
    {
        public CommunityMappingProfile()
        {
            CreateMap<Community, CommunityDto>()
                   .ForMember(dest => dest.Status, opt => opt.Ignore())
                  .ForMember(dest => dest.CommunityType, opt => opt.Ignore());

            CreateMap<CommunityDto, Community>()
                    .ForMember(dest => dest.Status, opt => opt.Ignore())
                    .ForMember(dest => dest.CommunityType, opt => opt.Ignore());
        }
    }
}