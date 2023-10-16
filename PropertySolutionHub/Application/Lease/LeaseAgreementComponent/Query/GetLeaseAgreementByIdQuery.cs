using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;

namespace PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Query
{
    public class GetLeaseAgreementByIdQuery : IRequest<LeaseAgreement>
    {
        public int Id { get; set; }
    }
}
