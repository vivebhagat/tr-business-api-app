using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Query
{
    public class GetAllFeaturedPropertiesQuery : IRequest<List<Property>>
    {
    }
}
