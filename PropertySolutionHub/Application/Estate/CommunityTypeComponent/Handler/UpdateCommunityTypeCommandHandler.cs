using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Handler
{
    public class UpdateCommunityTypeCommandHandler : IRequestHandler<UpdateCommunityTypeCommand, CommunityType>
    {
        ICommunityTypeRepository _communityTypeRepository;
        private readonly IMapper _mapper;

        public UpdateCommunityTypeCommandHandler(ICommunityTypeRepository communityTypeRepository, IMapper mapper)
        {
            _communityTypeRepository = communityTypeRepository;
            _mapper = mapper;
        }

        public async Task<CommunityType> Handle(UpdateCommunityTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityTypeRepository.UpdateCommunityType(request.CommunityType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating community type: " + ex.Message);
            }
        }
    }
}
