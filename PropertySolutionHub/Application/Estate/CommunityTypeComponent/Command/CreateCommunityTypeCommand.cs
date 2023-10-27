using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command
{
    public class CreateCommunityTypeCommand : IRequest<int>
    {
        public CommunityType CommunityType { get; set; }
    }
}
