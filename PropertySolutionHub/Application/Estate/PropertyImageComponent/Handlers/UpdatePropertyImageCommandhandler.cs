using MediatR;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Handler

{
    public class UpdatePropertyImageCommandHandler : IRequestHandler<UpdatePropertyImageCommand, PropertyImage>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public UpdatePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<PropertyImage> Handle(UpdatePropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.UpdatePropertyImage(request.PropertyImage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating proeprty Image: " + ex.Message);
            }
        }
    }
}
