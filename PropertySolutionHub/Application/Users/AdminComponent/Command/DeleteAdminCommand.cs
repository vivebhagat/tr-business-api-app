using MediatR;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class DeleteAdminCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
