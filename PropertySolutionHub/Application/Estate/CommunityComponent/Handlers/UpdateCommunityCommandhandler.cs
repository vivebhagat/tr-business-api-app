using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;


namespace PropertySolutionHub.Application.Estate.CommunityComponent.Handler
{
    public class UpdateCommunityCommandHandler : IRequestHandler<UpdateCommunityCommand, Community>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;


        public UpdateCommunityCommandHandler(ICommunityRepository communityRepository, IMapper mapper, IHttpHelper httpHelper, ICommunityToPropertyMapRepository communityToPropertyMapRepository)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
        }

        public async Task<Community> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var communityEntity = _mapper.Map<Community>(request.Community);
                Community community = await _communityRepository.UpdateCommunity(communityEntity, request.CommunityImage);

                if (community != null)
                {
                    if (request.CommunityToPropertyMapList.Count != 0)
                    {
                        foreach (var item in request.CommunityToPropertyMapList)
                        {
                            var communityToPropertyMapEntity = _mapper.Map<CommunityToPropertyMap>(item);

                            if (communityToPropertyMapEntity.Id == 0)
                            {
                                communityToPropertyMapEntity.CommunityId = community.Id;
                                await _communityToPropertyMapRepository.CreateCommunityToPropertyMap(communityToPropertyMapEntity);
                            }
                            else
                            {
                                await _communityToPropertyMapRepository.UpdateCommunityToPropertyMap(communityToPropertyMapEntity);
                            }
                        }

                        await _communityToPropertyMapRepository.UpdateCommunitySummaryDetails(community.Id);
                    }

                    string postData = JsonConvert.SerializeObject(request);
                    var result = await _communityRepository.UpdateRemoteCommunity(postData);

                }

                return community;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty: " + ex.Message);
            }
        }
    }
}
