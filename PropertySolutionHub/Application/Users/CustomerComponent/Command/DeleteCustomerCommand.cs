using MediatR;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Command
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
