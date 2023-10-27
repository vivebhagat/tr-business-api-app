using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command
{
    public class UpdateConstructionStatusCommand : IRequest<ConstructionStatus>
    {
        public ConstructionStatus ConstructionStatus { get; set; }
    }
}
