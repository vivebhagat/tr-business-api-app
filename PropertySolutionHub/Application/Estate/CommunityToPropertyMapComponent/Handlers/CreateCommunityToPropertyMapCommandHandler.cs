using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Users.CommunityToPropertyMapComponent.Handler
{
    public class CreateCommunityToPropertyMapCommandHandler : IRequestHandler<CreateCommunityToPropertyMapCommand, int>
    {
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;

        public CreateCommunityToPropertyMapCommandHandler(ICommunityToPropertyMapRepository communityToPropertyMapRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreateCommunityToPropertyMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var communityToPropertyMapEntity = _mapper.Map<CommunityToPropertyMap>(request.CommunityToPropertyMap);
                int communityToPropertyMapId = await _communityToPropertyMapRepository.CreateCommunityToPropertyMap(communityToPropertyMapEntity);

                return communityToPropertyMapId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating communityToPropertyMap: " + ex.Message);
            }
        }
    }
}