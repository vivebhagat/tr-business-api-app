using AutoMapper;
using CommunityTypeSolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Handler
{
    public class GetAllCommunityTypesQueryHandler : IRequestHandler<GetAllCommunityTypesQuery, List<CommunityType>>
    {
        ICommunityTypeRepository _communityTypeRepository;
        private readonly IMapper _mapper;

        public GetAllCommunityTypesQueryHandler(ICommunityTypeRepository communityTypeRepository, IMapper mapper)
        {
            _communityTypeRepository = communityTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<CommunityType>> Handle(GetAllCommunityTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityTypeRepository.GetAllCommunityTypes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community type: " + ex.Message);
            }
        }
    }
}
