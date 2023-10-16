

using MediatR;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionHub.Domain.Repository.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Handler
{
    public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, bool>
    {
        private readonly IPropertyImageRepository _propertyImageRepository;

        public DeletePropertyImageCommandHandler(IPropertyImageRepository propertyImageRepository)
        {
            _propertyImageRepository = propertyImageRepository;
        }

        public async Task<bool> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _propertyImageRepository.DeletePropertyImage(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty image: " + ex.Message);
            }
        }
    }
}
