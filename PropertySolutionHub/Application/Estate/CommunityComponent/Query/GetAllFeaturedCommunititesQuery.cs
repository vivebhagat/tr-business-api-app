using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Query
{
    public class GetAllFeaturedCommunitiesQuery : IRequest<List<Community>>
    {
    }
}
