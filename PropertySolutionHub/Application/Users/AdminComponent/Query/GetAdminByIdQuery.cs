using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Query
{
    public class GetAdminByIdQuery : IRequest<Admin>
    {
        public int Id { get; set; }
    }
}
