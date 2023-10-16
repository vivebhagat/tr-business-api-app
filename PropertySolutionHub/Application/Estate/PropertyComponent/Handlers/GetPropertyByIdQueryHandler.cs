using AutoMapper;
using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Handlers
{
    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, CreatePropertyCommand>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<CreatePropertyCommand> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var proeprtyData =  await _propertyRepository.GetPropertyById(request.Id);
                var propertyEntity = _mapper.Map<PropertyDto>(proeprtyData);

                CreatePropertyCommand data = new CreatePropertyCommand();
                data.Property = propertyEntity;

                return data;

            }
            catch (Exception ex)
            {
                throw new Exception("Error getting property: " + ex.Message);
            }
        }
    }
}