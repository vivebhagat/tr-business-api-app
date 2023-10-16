using MediatR;
using PropertySolutionHub.Api.Dto.Users;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Command
{
    public class UpdateBusinessUserCommand : IRequest<BusinessUser>
    {
        public BusinessUserDto BusinessUser { get; set; }
        public List<BusinessUserToRoleMapDto> BusinessUserToRoleMapList { get; set; }

    }
}
