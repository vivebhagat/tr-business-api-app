using MediatR;
using PropertySolutionHub.Api.Dto.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Command
{
    public class CreateContractRequestCommand : IRequest<int>
    {
        public ContractRequestDto ContractRequest { get; set; }
        public string DomainKey { get; set; }

    }
}
