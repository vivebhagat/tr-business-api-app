using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command
{
    public class CreateConstructionStatusCommand : IRequest<int>
    {
        public ConstructionStatus ConstructionStatus { get; set; }
    }
}
