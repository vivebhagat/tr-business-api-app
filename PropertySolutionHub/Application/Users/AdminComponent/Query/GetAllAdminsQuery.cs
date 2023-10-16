using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.AdminComponent.Query
{
    public class GetAllAdminsQuery : IRequest<List<Admin>>
    {
    }
}
