using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Command
{
    public class UpdateContractRequestCommand : IRequest<ContractRequest>
    {
        public ContractRequestDto ContractRequest { get; set; }
        public string DomainKey { get; set; }

    }
}
