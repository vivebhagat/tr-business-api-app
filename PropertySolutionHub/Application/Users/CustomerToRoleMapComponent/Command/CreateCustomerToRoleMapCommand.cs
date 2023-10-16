using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Command
{
    public class CreateCustomerToRoleMapCommand : IRequest<int>
    {
        public CustomerToRoleMap CustomerToRoleMap { get; set; }
    }
}
