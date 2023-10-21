using MediatR;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Setup;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Setup.Org.Command
{
    public class UpdateOrganizationCommand : IRequest<Organization>
    {
        public Organization Organization { get; set; }
        public IFormFile OrgImage { get; set; }

    }
}
