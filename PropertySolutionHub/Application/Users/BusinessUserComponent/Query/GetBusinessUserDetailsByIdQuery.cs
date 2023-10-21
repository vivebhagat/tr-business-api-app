using MediatR;
using PropertySolutionHub.Application.QueryModels.User;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserComponent.Query
{
    public class GetBusinessUserDetailsByIdQuery : IRequest<BusinessUser>
    {
        public int Id { get; set; }
    }
}