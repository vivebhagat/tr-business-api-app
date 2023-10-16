using AutoMapper;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Domain.MappingProfile.Estate
{
    public class ContractRequestMappingProfile : Profile
    {
        public ContractRequestMappingProfile()
        {
            CreateMap<ContractRequestDto, ContractRequest>();

            CreateMap<ContractRequest, ContractRequestDto>();
        }
    }
}