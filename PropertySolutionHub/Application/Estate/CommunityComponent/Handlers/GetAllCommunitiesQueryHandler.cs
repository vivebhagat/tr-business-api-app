using AutoMapper;
using MediatR;
using CommunitySolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Application.Estate.CommunityComponent.Query;

namespace CommunitySolutionHub.Application.Estate.CommunityComponent.Handler
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllCommunititesQuery, List<Community>>
    {
        private readonly ICommunityRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(ICommunityRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Community>> Handle(GetAllCommunititesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetAllCommunities();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property list: " + ex.Message);
            }
        }
    }
}

