using MediatR;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionHub.Domain.Repository.Estate;


namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Handler
{
    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommand, int>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public CreatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<int> Handle(CreatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.CreatePropertyImage(request.PropertyImage, request.Name, request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty image: " + ex.Message);
            }
        }
    }
}
