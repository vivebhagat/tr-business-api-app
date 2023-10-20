using AutoMapper;
using MediatR;
using OrganizationSolutionHub.Domain.Repository.Estate;
using PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Setup;

namespace PropertySolutionHub.Application.Users.Org.Handler
{
    public class GetAllOrganizationQueryHandler : IRequestHandler<GetAllOrganizationQuery, List<Organization>>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMapper _mapper;

        public GetAllOrganizationQueryHandler(IOrganizationRepository organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task<List<Organization>> Handle(GetAllOrganizationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _organizationRepository.GetAllOrganizations();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting organization: " + ex.Message);
            }
        }
    }
}