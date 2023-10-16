using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class CreateAdminToRoleMapCommand : IRequest<int>
    {
        public AdminToRoleMap AdminToRoleMap { get; set; }
    }
}
