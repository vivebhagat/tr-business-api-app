using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class AdminLoginCommand : IRequest<AuthResponse>
    {
        public LoginRequestDto LoginRequestDto { get; set; }
    }
}
