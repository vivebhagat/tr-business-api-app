using MediatR;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Query
{
    public class GetCommunityTypeByIdQuery : IRequest<CommunityType>
    {
        public int Id { get; set; }
    }
}
