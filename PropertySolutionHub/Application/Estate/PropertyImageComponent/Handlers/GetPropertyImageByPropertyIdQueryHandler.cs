using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Handlers
{
    public class GetPropertyImageByPropertyIdQueryHandler : IRequestHandler<GetPropertyImageByPropertyIdQuery, List<PropertyImage>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public GetPropertyImageByPropertyIdQueryHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyImage>> Handle(GetPropertyImageByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.GetPropertyImageByPropertyId(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property image: " + ex.Message);
            }
        }
    }
}