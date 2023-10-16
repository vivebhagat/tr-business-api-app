using MediatR;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.BusinessUserToDomainKeyMapComponent.Query
{
    public class ValidateDomainKeyQuery : IRequest<DomainDetail>
    {
        public int Id { get; set; }
    }
}
