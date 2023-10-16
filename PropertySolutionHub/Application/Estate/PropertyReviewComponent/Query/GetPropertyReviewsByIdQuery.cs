using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Query
{
    public class GetPropertyReviewsByIdQuery : IRequest<PropertyReview>
    {
        public int Id { get; set; }
    }
}
