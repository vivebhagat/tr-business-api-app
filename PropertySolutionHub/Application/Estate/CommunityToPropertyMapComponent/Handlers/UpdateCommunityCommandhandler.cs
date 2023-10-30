using AutoMapper;
using PropertySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;


namespace PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Handler
{
    public class UpdateCommunityToPropertyMapCommandHandler : IRequestHandler<UpdateCommunityToPropertyMapCommand, CommunityToPropertyMap>
    {
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;


        public UpdateCommunityToPropertyMapCommandHandler(ICommunityToPropertyMapRepository communityToPropertyMapRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<CommunityToPropertyMap> Handle(UpdateCommunityToPropertyMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var communityToPropertyMapEntity = _mapper.Map<CommunityToPropertyMap>(request.CommunityToPropertyMap);
                CommunityToPropertyMap communityToPropertyMap = await _communityToPropertyMapRepository.UpdateCommunityToPropertyMap(communityToPropertyMapEntity);
                return communityToPropertyMap;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty: " + ex.Message);
            }
        }
    }
}
