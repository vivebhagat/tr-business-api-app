using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using MediatR;


namespace PropertySolutionHub.Application.Estate.CommunityComponent.Query
{
    public class GetCommunityToPropertyMapByIdQuery : IRequest<CreateCommunityToPropertyMapCommand>
    {
        public int Id { get; set; }
    }
}
