using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class BusinessUserRoleCommand : IRequest<RoleAuthResponse>
    {
        public RoleSelectionRequestDto RoleSelectionRequestDto { get; set; }
    }
}
