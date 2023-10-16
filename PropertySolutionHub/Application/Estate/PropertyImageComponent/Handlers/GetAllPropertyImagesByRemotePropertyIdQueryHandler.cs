using AutoMapper;
using MediatR;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Handler
{
    public class GetAllPropertyImagesByRemotePropertyIdQueryHandler : IRequestHandler<GetAllPropertyImagesByRemoteProeprtyIdQuery, List<PropertyImage>>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyImagesByRemotePropertyIdQueryHandler(IPropertyImageRepository propertyImageRepository, IMapper mapper)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyImage>> Handle(GetAllPropertyImagesByRemoteProeprtyIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.GetPropertyImageByRemotePropertyId(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property image list: " + ex.Message);
            }
        }
    }
}

