using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Query
{
    public class GetAllBusinessUsersQuery : IRequest<List<BusinessUser>>
    {
    }
}
