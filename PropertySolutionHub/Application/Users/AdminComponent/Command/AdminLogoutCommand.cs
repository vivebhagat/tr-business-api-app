using MediatR;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class AdminLogoutCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}