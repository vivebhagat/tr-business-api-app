using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Command
{
    public class CreateCommunityToPropertyMapCommand : IRequest<int>
    {
        public Community Community { get; set; }
    }
}
