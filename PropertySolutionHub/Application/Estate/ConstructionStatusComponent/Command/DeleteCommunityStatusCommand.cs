using MediatR;


namespace PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command
{
    public class DeleteConstructionStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
