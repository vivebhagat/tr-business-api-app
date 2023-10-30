using AutoMapper;
using PropertySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Handler
{
    public class CreateCommunityTypeCommandHandler : IRequestHandler<CreateCommunityTypeCommand, int>
    {
        ICommunityTypeRepository _communityTypeRepository;
        private readonly IMapper _mapper;

        public CreateCommunityTypeCommandHandler(ICommunityTypeRepository communityTypeRepository, IMapper mapper)
        {
            _communityTypeRepository = communityTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCommunityTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityTypeRepository.CreateCommunityType(request.CommunityType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating community type: " + ex.Message);
            }
        }
    }
}
