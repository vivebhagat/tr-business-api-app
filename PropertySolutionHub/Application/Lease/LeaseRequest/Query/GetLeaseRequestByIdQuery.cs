using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;

namespace PropertySolutionHub.Application.Estate.LeaseRequestComponent.Query
{
    public class GetLeaseRequestByIdQuery : IRequest<LeaseRequest>
    {
        public int Id { get; set; }
    }
}
