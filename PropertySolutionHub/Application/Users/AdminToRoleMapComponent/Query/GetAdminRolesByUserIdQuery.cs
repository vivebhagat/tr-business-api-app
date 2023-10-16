using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.AdminToRoleMapComponent.Query
{
    public class GetAdminRolesByUserIdQuery : IRequest<List<AdminToRoleMap>>
    {
        public string UserId { get; set; }
    }
}