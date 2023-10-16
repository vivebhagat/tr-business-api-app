using MediatR;
using PropertySolutionHub.Application.Estate.PropertyReviewComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Handlers
{

    public class UpdatePropertyReviewCommandHandler : IRequestHandler<UpdatePropertyReviewCommand, PropertyReview>
    {
        private readonly IPropertyReviewRepository _propertyRepository;

        public UpdatePropertyReviewCommandHandler(IPropertyReviewRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyReview> Handle(UpdatePropertyReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.UpdatePropertyReview(request.PropertyReview);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty review: " + ex.Message);
            }
        }
    }
}