using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Users.CommunityToPropertyMapComponent.Handler
{
    public class DeleteCommunityToPropertyMapCommandHandler : IRequestHandler<DeleteCommunityToPropertyMapCommand, bool>
    {
        private readonly ICommunityToPropertyMapRepository _communityToPropertyMapRepository;
        ICommunityRepository _communityRepository;
        IHttpHelper httpHelper;
        IMapper _mapper;

        public DeleteCommunityToPropertyMapCommandHandler(ICommunityToPropertyMapRepository communityToPropertyMapRepository, IHttpHelper httpHelper, IMapper mapper, ICommunityRepository communityRepository)
        {
            _communityToPropertyMapRepository = communityToPropertyMapRepository;
            this.httpHelper = httpHelper;
            _mapper = mapper;
            _communityRepository = communityRepository;
        }

        public async Task<bool> Handle(DeleteCommunityToPropertyMapCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Community community = await _communityToPropertyMapRepository.DeleteCommunityToPropertyMap(request.Id);

                var communityDto = _mapper.Map<CommunityDto>(community);

                communityDto.PriceFrom = community.PriceFrom;
                communityDto.PriceTo = community.PriceFrom;
                communityDto.BedFrom = community.BedFrom;
                communityDto.BedTo = community.BedTo;
                communityDto.BathFrom = community.BathFrom;
                communityDto.BathTo = community.BathTo;
                communityDto.AreaFrom = community.AreaFrom;
                communityDto.AreaTo = community.AreaTo;
                communityDto.NumberOfUnits = community.NumberOfUnits;

                UpdateCommunityCommand updateCommunityCommand = new UpdateCommunityCommand
                {
                    Community = communityDto,
                };

                string postData = JsonConvert.SerializeObject(updateCommunityCommand);
                var result = await _communityRepository.UpdateRemoteCommunity(postData);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting property: " + ex.Message);
            }
        }
    }

}
