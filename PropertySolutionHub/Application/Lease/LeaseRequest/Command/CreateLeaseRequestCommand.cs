using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;

namespace PropertySolutionHub.Application.Estate.LeaseRequestComponent.Command
{
    public class CreateLeaseRequestCommand : IRequest<int>
    {
        public LeaseRequest LeaseRequest { get; set; }

    }
}
