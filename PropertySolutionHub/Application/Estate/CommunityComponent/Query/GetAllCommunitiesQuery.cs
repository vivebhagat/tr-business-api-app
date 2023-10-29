using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Query
{
    public class GetAllCommunititesQuery : IRequest<List<Community>>
    {
    }
}
