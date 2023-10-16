using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class CreateAdminCommand : IRequest<int>
    {
        public Admin Admin { get; set; }
        public string DataRoute { get; set; }

    }
}
