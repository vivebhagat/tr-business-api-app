using MediatR;


namespace PropertySolutionHub.Application.Estate.CommunityComponent.Command
{
    public class DeleteCommunityCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
