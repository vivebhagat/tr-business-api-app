using MediatR;


namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Command
{
    public class DeletePropertyImageCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
