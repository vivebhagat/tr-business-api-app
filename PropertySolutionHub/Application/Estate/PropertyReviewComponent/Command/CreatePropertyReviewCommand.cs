using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Command
{
    public class CreatePropertyReviewCommand : IRequest<int>
    {
        public PropertyReview PropertyReview { get; set; }

    }
}
