using MediatR;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Query
{
    public class GetConstructionStatusByIdQuery : IRequest<ConstructionStatus>
    {
        public int Id { get; set; }
    }
}
