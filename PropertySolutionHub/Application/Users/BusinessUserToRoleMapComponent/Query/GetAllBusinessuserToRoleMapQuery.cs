using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query
{
    public class GetAllBusinessuserToRoleMapQuery : IRequest<List<BusinessUserRole>>
    {
    }
}