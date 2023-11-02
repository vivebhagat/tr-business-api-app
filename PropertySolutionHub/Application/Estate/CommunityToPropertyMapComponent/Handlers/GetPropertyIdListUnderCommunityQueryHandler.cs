using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Query;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Handlers
{
    public class GetPropertyIdListUnderCommunityQueryHandler : IRequestHandler<GetPropertyIdListUnderCommunityQuery, List<int>>
    {
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        private readonly IMapper _mapper;


        public GetPropertyIdListUnderCommunityQueryHandler(ICommunityToPropertyMapRepository communityToPropertyMapRepository, IMapper mapper)
        {
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
            _mapper = mapper;
        }

        public async Task<List<int>> Handle(GetPropertyIdListUnderCommunityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityToPropertyMapRepository.GetPropertyIdListunderCommunity(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
