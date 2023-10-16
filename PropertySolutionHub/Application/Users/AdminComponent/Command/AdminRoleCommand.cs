using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class AdminRoleCommand : IRequest<RoleAuthResponse>
    {
        public RoleSelectionRequestDto RoleSelectionRequestDto { get; set; }
    }
}
