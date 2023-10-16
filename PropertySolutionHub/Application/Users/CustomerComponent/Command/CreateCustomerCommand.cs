using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Command
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public Customer Customer { get; set; }
        public string DataRoute { get; set; }

    }
}
