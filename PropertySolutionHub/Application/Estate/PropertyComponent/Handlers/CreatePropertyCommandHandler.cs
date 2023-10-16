using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Users.PropertyComponent.Handler
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;
        IHttpHelper httpHelper;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IMapper mapper, IPropertyImageRepository propertyImageRepository, IHttpHelper httpHelper)
        {
            _propertyRepository = propertyRepository;
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
            this.httpHelper = httpHelper;
        }

        public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var propertyEntity = _mapper.Map<Property>(request.Property);
                int propertyId = await _propertyRepository.CreateProperty(propertyEntity, request.PropertyImage);

                if (propertyId != 0)
                {
                    string postData = JsonConvert.SerializeObject(request);
                    bool result = await _propertyRepository.UpdateRemoteId(postData, propertyId);
                }

                return propertyId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating property: " + ex.Message);
            }
        }
    }
}