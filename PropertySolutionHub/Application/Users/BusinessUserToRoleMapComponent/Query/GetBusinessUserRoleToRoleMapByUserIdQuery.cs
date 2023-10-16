using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query
{
    public class GetBusinessUserToRoleMapByUserIdQuery : IRequest<List<BusinessUserToRoleMap>>
    {
        public string UserId { get; set; }
    }
}