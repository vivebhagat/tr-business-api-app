using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;

namespace PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Command
{
    public class CreateLeaseAgreementCommand : IRequest<int>
    {
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}
