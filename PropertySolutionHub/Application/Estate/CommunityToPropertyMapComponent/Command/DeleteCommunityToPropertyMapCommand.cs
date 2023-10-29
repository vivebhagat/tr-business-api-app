using MediatR;


namespace PropertySolutionHub.Application.Estate.CommunityComponent.Command
{
    public class DeleteCommunityToProeprtyMapCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
