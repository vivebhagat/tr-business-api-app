using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;

namespace PropertySolutionHub.Application.Users.LeaseRequestComponent.Command
{
    public class UpdateLeaseRequestCommand : IRequest<LeaseRequest>
    {
        public LeaseRequest LeaseRequest { get; set; }
    }
}
