using AutoMapper;
using CommunityTypeSolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Handler
{
    public class DeleteCommunityTypeCommandHandler : IRequestHandler<DeleteCommunityTypeCommand, bool>
    {
        ICommunityTypeRepository _communityTypeRepository;
        private readonly IMapper _mapper;

        public DeleteCommunityTypeCommandHandler(ICommunityTypeRepository communityTypeRepository, IMapper mapper)
        {
            _communityTypeRepository = communityTypeRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteCommunityTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityTypeRepository.DeleteCommunityType(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting community type: " + ex.Message);
            }
        }
    }
}
