using MediatR;
using PropertySolutionHub.Api.Dto.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class CreateBusinessUserRoleCommand : IRequest<int>
    {
        public BusinessUserRoleDto BusinessUserRole { get; set; }
    }
}
