using MediatR;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class BusinessUserLogoutCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}