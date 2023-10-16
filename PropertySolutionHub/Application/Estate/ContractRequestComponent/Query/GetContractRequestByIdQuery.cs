using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Query
{
    public class GetContractRequestByIdQuery : IRequest<ContractRequest>
    {
        public int Id { get; set; }
    }
}
