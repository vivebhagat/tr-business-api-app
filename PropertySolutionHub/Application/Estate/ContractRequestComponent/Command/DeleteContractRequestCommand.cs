using MediatR;

namespace PropertySolutionHub.Application.Estate.ContractRequestComponent.Command
{
    public class DeleteContractRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
