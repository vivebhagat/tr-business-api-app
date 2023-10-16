using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Query
{
    public class GetAllCustomersQuery : IRequest<List<Customer>>
    {
    }
}
