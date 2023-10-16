using MediatR;

namespace PropertySolutionHub.Application.Users.CustomerComponent.Command
{
    public class CustomerLogoutCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}