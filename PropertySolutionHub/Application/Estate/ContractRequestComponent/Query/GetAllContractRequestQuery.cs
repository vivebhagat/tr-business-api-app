using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Query
{
    public class GetAllContractRequestsQuery : IRequest<List<ContractRequest>>
    {
    }
}
