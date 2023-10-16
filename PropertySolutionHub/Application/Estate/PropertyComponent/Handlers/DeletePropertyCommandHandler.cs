using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Helper;
using PropertySolutionHub.Domain.Repository.Estate;


namespace PropertySolutionHub.Application.Users.PropertyComponent.Handler
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        IHttpHelper httpHelper;

        public DeletePropertyCommandHandler(IPropertyRepository propertyRepository, IHttpHelper httpHelper)
        {
            _propertyRepository = propertyRepository;
            this.httpHelper = httpHelper;
        }

        public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int remoteId = await _propertyRepository.DeleteProperty(request.Id);

                if (remoteId != 0)
                {
                    var result = await _propertyRepository.DeleteRemoteProperty(remoteId);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating proeprty: " + ex.Message);
            }
        }
    }

}
