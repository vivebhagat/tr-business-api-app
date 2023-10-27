using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command
{
    public class UpdateCommunityTypeCommand : IRequest<CommunityType>
    {
        public CommunityType CommunityType { get; set; }
    }
}
