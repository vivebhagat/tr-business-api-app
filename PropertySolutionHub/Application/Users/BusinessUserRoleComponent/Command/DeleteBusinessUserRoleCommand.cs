using MediatR;
using PropertySolutionHub.Api.Dto.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Command
{
    public class DeleteBusinessUserRoleCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
