using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command
{
    public class UpdateCommunityToPropertyMapCommand : IRequest<CommunityToPropertyMap>
    {
        public CommunityToPropertyMap CommunityToPropertyMap { get; set; }
    }
}
