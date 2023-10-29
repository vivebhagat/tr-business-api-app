using AutoMapper;
using CommunitySolutionHub.Domain.Repository.Estate;
using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Application.Estate.CommunityComponent.Query;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Handlers
{
    public class GetCommunityByIdQueryHandler : IRequestHandler<GetCommunityByIdQuery, CreateCommunityCommand>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;

        public GetCommunityByIdQueryHandler(ICommunityRepository communityRepository, IMapper mapper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
        }

        public async Task<CreateCommunityCommand> Handle(GetCommunityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var communityData =  await _communityRepository.GetCommunityById(request.Id);
                CreateCommunityCommand data = new CreateCommunityCommand();
                data.Community = communityData;

                return data;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community: " + ex.Message);
            }
        }
    }
}