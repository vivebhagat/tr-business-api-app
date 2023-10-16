using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class BusinessUserLoginCommand : IRequest<AuthResponse>
    {
        public LoginRequestDto LoginRequestDto { get; set; }
    }
}
