using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query
{
    public class GetCustomerRolesByUserIdQuery : IRequest<List<CustomerToRoleMap>>
    {
        public string UserId { get; set; }
    }
}