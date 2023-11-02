using MediatR;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;

namespace PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Query
{
    public class GetPropertyIdListUnderCommunityQuery : IRequest<List<int>>
    {
        public int Id { get; set; }
    }
}

