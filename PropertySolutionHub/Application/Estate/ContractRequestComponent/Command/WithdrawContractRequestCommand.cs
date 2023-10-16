using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Command
{
    public class WithdrawContractRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string DomainKey { get; set; }
    }
}