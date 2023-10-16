using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Command
{
    public class UpdatePropertyReviewCommand : IRequest<PropertyReview>
    {
        public PropertyReview PropertyReview { get; set; }

    }
}
