using AutoMapper;
using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Application.Estate.CommunityComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Handlers
{
    public class GetCommunityByIdQueryHandler : IRequestHandler<GetCommunityByIdQuery, CreateCommunityCommand>
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        private readonly IMapper _mapper;

        public GetCommunityByIdQueryHandler(ICommunityRepository communityRepository, IMapper mapper, ICommunityToPropertyMapRepository communityToPropertyMapRepository)
        {
            _communityRepository = communityRepository;
            _mapper = mapper;
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
        }

        public async Task<CreateCommunityCommand> Handle(GetCommunityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var communityData =  await _communityRepository.GetCommunityById(request.Id);
                var communityEntity = _mapper.Map<CommunityDto>(communityData);

                var communityToPropertyMapData = await _communityToPropertyMapRepository.GetPropertyListForCommunity(request.Id);
                var communityToProeprtyMapEntities = new List<CommunityToPropertyMapDto>();

                foreach (var item in communityToPropertyMapData)
                {
                    var communityToProeprtyMapEntity = _mapper.Map<CommunityToPropertyMapDto>(item);
                    communityToProeprtyMapEntities.Add(communityToProeprtyMapEntity);
                }

                CreateCommunityCommand data = new CreateCommunityCommand();
                data.Community = communityEntity;
                data.CommunityToPropertyMapList = communityToProeprtyMapEntities;

                return data;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting community: " + ex.Message);
            }
        }
    }
}