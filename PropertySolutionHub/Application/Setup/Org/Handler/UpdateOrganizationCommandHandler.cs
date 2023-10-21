using AutoMapper;
using MediatR;
using OrganizationSolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Application.Setup.Org.Command;
using PropertySolutionHub.Domain.Entities.Setup;

namespace PropertySolutionHub.Application.Users.Org.Handler
{
    public class updateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _organization;
        private readonly IMapper _mapper;

        public updateOrganizationCommandHandler(IOrganizationRepository organization, IMapper mapper)
        {
            _organization = organization;
            _mapper = mapper;
        }

        public async Task<Organization> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _organization.UpdateOrganization(request.Organization, request.OrgImage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating organization: " + ex.Message);
            }
        }
    }
}
