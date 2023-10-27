using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Query
{
    public class GetAllCommunityTypesQuery : IRequest<List<CommunityType>>
    {
    }
}
