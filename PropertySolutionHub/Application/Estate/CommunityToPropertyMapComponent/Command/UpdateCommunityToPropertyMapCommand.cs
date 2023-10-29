using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Command
{
    public class UpdateCommunityToProeprtyMapCommand : IRequest<CommunityToPropertyMap>
    {
        public CommunityToPropertyMap Community { get; set; }
    }
}
