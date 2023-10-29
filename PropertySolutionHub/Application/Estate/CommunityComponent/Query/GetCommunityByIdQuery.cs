using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using MediatR;


namespace PropertySolutionHub.Application.Estate.CommunityComponent.Query
{
    public class GetCommunityByIdQuery : IRequest<CreateCommunityCommand>
    {
        public int Id { get; set; }
    }
}
