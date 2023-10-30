using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command
{
    public class CreateCommunityToPropertyMapCommand : IRequest<int>
    {
        public CommunityToPropertyMap CommunityToPropertyMap { get; set; }
    }
}
