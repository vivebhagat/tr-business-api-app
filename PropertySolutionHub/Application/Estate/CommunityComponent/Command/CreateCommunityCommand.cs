using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityComponent.Command
{
    public class CreateCommunityCommand : IRequest<int>
    {
        public Community Community { get; set; }
        public IFormFile CommunityImage { get; set; }
    }
}
