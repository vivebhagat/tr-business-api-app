using MediatR;
using PropertySolutionHub.Api.Dto.Users;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Command
{
    public class UpdateBusinessUserRoleCommand : IRequest<BusinessUserRole>
    {
        public BusinessUserRoleDto BusinessUserRole { get; set; }
    }
}