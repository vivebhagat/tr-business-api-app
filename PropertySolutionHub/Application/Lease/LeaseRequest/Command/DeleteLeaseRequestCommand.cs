using MediatR;

namespace PropertySolutionHub.Application.Users.LeaseRequestComponent.Command
{
    public class DeleteLeaseRequestCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
