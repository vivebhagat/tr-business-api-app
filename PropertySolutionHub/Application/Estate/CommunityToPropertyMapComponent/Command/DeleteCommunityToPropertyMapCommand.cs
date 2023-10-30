using MediatR;


namespace PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command
{
    public class DeleteCommunityToPropertyMapCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
