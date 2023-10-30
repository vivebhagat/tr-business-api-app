using AutoMapper;
using PropertySolutionHub.Domain.Repository.Estate;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Handler
{
    public class UpdateConstructionStatusCommandHandler : IRequestHandler<UpdateConstructionStatusCommand, ConstructionStatus>
    {
        IConstructionStatusRepository _communityTypeRepository;
        private readonly IMapper _mapper;

        public UpdateConstructionStatusCommandHandler(IConstructionStatusRepository communityTypeRepository, IMapper mapper)
        {
            _communityTypeRepository = communityTypeRepository;
            _mapper = mapper;
        }

        public async Task<ConstructionStatus> Handle(UpdateConstructionStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _communityTypeRepository.UpdateConstructionStatus(request.ConstructionStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating construction status: " + ex.Message);
            }
        }
    }
}
