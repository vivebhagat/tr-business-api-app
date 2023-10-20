using AutoMapper;
using MediatR;
using OrganizationSolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Application.Estate.Org.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Entities.Setup;

namespace PropertySolutionHub.Application.Setup.Org.Handler
{
    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Organization>
    {
        private readonly IOrganizationRepository _organization;
        private readonly IMapper _mapper;

        public GetOrganizationByIdQueryHandler(IOrganizationRepository organization, IMapper mapper)
        {
            _organization = organization;
            _mapper = mapper;
        }

        public async Task<Organization> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _organization.GetOrganizationById(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting organization: " + ex.Message);
            }
        }
    }
}