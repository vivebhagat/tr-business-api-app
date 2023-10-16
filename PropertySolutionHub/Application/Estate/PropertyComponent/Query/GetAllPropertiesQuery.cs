using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Query
{
    public class GetAllPropertiesQuery : IRequest<List<Property>>
    {
    }
}
