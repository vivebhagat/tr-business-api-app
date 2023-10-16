using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Command
{
    public class UpdateAdminCommand : IRequest<Admin>
    {
        public Admin Admin { get; set; }
    }
}
