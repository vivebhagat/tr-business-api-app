using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Query
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; set; }
    }
}
