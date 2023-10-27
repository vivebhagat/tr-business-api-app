using MediatR;


namespace PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command
{
    public class DeleteCommunityTypeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
