using MediatR;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class DeleteBusinessUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
