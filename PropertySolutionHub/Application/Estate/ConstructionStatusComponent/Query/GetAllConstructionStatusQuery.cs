using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Query
{
    public class GetAllConstructionStatusQuery : IRequest<List<ConstructionStatus>>
    {
    }
}
