using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Query
{
    public class GetCustomerByUserIdQuery : IRequest<Customer>
    {
        public string UserId { get; set; }
    }
}
