using AutoMapper;
using CommunitySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;

namespace PropertySolutionHub.Application.Users.CommunityComponent.Handler
{
    public class CreateCommunityCommandHandler : IRequestHandler<CreateCommunityCommand, int>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;

        public CreateCommunityCommandHandler(ICommunityRepository communityRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var communityEntity = _mapper.Map<Community>(request.Community);
                int communityId = await _communityRepository.CreateCommunity(communityEntity, request.CommunityImage);

                if (communityId != 0)
                {
                    string postData = JsonConvert.SerializeObject(request);
                    bool result = await _communityRepository.UpdateRemoteId(postData, communityId);
                }

                return communityId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating community: " + ex.Message);
            }
        }
    }
}