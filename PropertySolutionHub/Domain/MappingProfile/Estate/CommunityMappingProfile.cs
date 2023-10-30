using AutoMapper;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Domain.MappingProfile.Estate
{
    public class CommunityMappingProfile : Profile
    {
        public CommunityMappingProfile()
        {
            CreateMap<Community, CommunityDto>();
            CreateMap<CommunityDto, Community>();
        }
    }
}