using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.CommunityComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Handlers
{
    public class GetAllFeaturedCommunitiesQueryHandler : IRequestHandler<GetAllFeaturedCommunitiesQuery, List<Community>>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public GetAllFeaturedCommunitiesQueryHandler(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }

        public async Task<List<Community>> Handle(GetAllFeaturedCommunitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityRepository.GetAllFeaturedCommunities();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community list: " + ex.Message);
            }
        }
    }
}

