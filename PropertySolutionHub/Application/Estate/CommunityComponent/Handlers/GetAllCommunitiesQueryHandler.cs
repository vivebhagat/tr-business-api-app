using AutoMapper;
using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Application.Estate.CommunityComponent.Query;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Handler
{
    public class GetAllCommunitiesQueryHandler : IRequestHandler<GetAllCommunitiesQuery, List<Community>>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public GetAllCommunitiesQueryHandler(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }

        public async Task<List<Community>> Handle(GetAllCommunitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityRepository.GetAllCommunities();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community list: " + ex.Message);
            }
        }
    }
}

