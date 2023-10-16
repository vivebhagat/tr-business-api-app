using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;


namespace PropertySolutionHub.Application.Estate.PropertyComponent.Handler
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Property>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;


        public UpdatePropertyCommandHandler(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IMapper mapper, IHttpHelper httpHelper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<Property> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propertyEntity = _mapper.Map<Property>(request.Property);
                Property property = await _propertyRepository.UpdateProperty(propertyEntity, request.PropertyImage);

                if (property != null)
                {
                    string postData = JsonConvert.SerializeObject(request);
                    var result = await _propertyRepository.UpdateRemoteProperty(postData);
                }

                return property;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty: " + ex.Message);
            }
        }
    }
}
