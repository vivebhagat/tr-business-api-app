using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class CreateBusinessUserToRoleMapCommand : IRequest<int>
    {
        public BusinessUserToRoleMap BusinessUserToRoleMap { get; set; }
    }
}
