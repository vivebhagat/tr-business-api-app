using MediatR;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Shared.BusinessUserToDomainKeyMapComponent.Query
{
    public class GetDomainKeyForUserQuery : IRequest<List<BaseApplicationUserToDomainKeyMap>>
    {
        public string Id { get; set; }
    }
}
