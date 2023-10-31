using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Users.CommunityComponent.Handler
{
    public class CreateCommunityCommandHandler : IRequestHandler<CreateCommunityCommand, int>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;

        public CreateCommunityCommandHandler(ICommunityRepository communityRepository, IMapper mapper, IHttpHelper httpHelper, ICommunityToPropertyMapRepository communityToPropertyMapRepository)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
        }

        public async Task<int> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CommunityToPropertyMapList.Count == 0)
                {
                    throw new Exception("Please add at least one proeprty to create community.");
                }

                var communityEntity = _mapper.Map<Community>(request.Community);
                int communityId = await _communityRepository.CreateCommunity(communityEntity, request.CommunityImage);

                if (communityId != 0)
                {

                    if (request.CommunityToPropertyMapList.Count != 0)
                    {
                        foreach (var item in request.CommunityToPropertyMapList)
                        {
                            var communityToPropertyMapEntity = _mapper.Map<CommunityToPropertyMap>(item);

                            if (communityToPropertyMapEntity.Id == 0)
                            {
                                communityToPropertyMapEntity.CommunityId = communityId;
                                await _communityToPropertyMapRepository.CreateCommunityToPropertyMap(communityToPropertyMapEntity);
                            }
                            else
                            {
                                return 0;
                            }
                        }

                       Community tempCommunity =  await _communityToPropertyMapRepository.UpdateCommunitySummaryDetails(communityId);
                        request.Community.PriceFrom = tempCommunity.PriceFrom;
                        request.Community.PriceTo = tempCommunity.PriceFrom;
                        request.Community.BedFrom = tempCommunity.BedFrom;
                        request.Community.BedTo = tempCommunity.BedTo;
                        request.Community.BathFrom = tempCommunity.BathFrom;
                        request.Community.BathTo = tempCommunity.BathTo;
                        request.Community.AreaFrom = tempCommunity.AreaFrom;
                        request.Community.AreaTo = tempCommunity.AreaTo;
                        request.Community.NumberOfUnits = tempCommunity.NumberOfUnits;

                    }

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