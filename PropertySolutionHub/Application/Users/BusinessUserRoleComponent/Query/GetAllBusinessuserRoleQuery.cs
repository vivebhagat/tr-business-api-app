using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query
{
    public class GetAllBusinessuserRolesQuery : IRequest<List<BusinessUserRole>>
    {
    }
}