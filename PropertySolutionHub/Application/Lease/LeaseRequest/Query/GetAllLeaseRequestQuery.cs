using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Estate.LeaseRequestComponent.Query
{
    public class GetAllLeaseRequestsQuery : IRequest<List<LeaseRequest>>
    {
    }
}
