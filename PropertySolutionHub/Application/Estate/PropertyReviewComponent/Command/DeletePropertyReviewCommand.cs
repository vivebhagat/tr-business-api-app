using MediatR;


namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Command
{
    public class DeletePropertyReviewCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
