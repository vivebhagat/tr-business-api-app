using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;

namespace PropertySolutionHub.Application.Users.LeaseAgreementComponent.Command
{
    public class UpdateLeaseAgreementCommand : IRequest<LeaseAgreement>
    {
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}
