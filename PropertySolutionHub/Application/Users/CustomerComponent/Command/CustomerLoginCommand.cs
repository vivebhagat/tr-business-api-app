using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Command
{
    public class CustomerLoginCommand : IRequest<RoleAuthResponse>
    {
        public LoginRequestDto LoginRequestDto { get; set; }
    }
}
