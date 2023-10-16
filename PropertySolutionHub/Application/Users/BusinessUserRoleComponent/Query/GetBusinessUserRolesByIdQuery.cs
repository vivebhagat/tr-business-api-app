using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query
{
    public class GetBusinessUserRolesByIdQuery : IRequest<BusinessUserRole>
    {
        public int Id { get; set; }
    }
}