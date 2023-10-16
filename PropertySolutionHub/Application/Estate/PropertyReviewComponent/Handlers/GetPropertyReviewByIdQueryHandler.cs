using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.PropertyReviewComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyReviewComponent.Handlers
{
    public class GetPropertyReviewByIdQueryHandler : IRequestHandler<GetPropertyReviewsByIdQuery, PropertyReview>
    {
        private readonly IPropertyReviewRepository _propertyReviewRepository;
        private readonly IMapper _mapper;

        public GetPropertyReviewByIdQueryHandler(IPropertyReviewRepository propertyReviewRepository, IMapper mapper)
        {
            _propertyReviewRepository = propertyReviewRepository;
            _mapper = mapper;
        }

        public async Task<PropertyReview> Handle(GetPropertyReviewsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyReviewRepository.GetPropertyReviewById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property review: " + ex.Message);
            }
        }
    }
}