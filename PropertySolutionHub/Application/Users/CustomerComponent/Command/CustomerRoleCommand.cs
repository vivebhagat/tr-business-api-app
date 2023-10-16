using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Command
{
    public class CustomerRoleCommand : IRequest<RoleAuthResponse>
    {
        public RoleSelectionRequestDto RoleSelectionRequestDto { get; set; }
    }
}
