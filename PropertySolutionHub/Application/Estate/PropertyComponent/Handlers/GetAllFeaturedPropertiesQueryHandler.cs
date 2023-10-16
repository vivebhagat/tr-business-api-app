using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Handlers
{
    public class GetAllFeaturedPropertiesQueryHandler : IRequestHandler<GetAllFeaturedPropertiesQuery, List<Property>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllFeaturedPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<Property>> Handle(GetAllFeaturedPropertiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyRepository.GetAllFeaturedProperties();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property list: " + ex.Message);
            }
        }
    }
}

