using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Query
{
    public class GetAllPropertyReviewsQuery : IRequest<List<PropertyReview>>
    {
    }
}
