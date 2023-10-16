using MediatR;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Query
{
    public class GetBusinessUserByUserIdQuery : IRequest<BusinessUser>
    {
        public string UserId { get; set; }
    }
}
