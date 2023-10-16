using MediatR;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Query
{
    public class GetAllLeaseAgreementsQuery : IRequest<List<LeaseAgreement>>
    {
    }
}
