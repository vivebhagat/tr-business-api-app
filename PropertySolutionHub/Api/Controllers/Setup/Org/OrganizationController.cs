using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.Org.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Application.Setup.Org.Command;
using PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Setup;

namespace PropertySolutionHub.Api.Controllers.Setup.Org
{

    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<Organization> EditOrganization(UpdateOrganizationCommand @object)
        {
            return await _mediator.Send(new UpdateOrganizationCommand { Organization = @object.Organization });
        }

        [HttpGet("[action]/{id}")]
        public async Task<Organization> GetOrganizationById(GetOrganizationByIdQuery @object)
        {
            return await _mediator.Send(new GetOrganizationByIdQuery { Id = @object.Id});
        }

        [HttpGet("[action]")]
        public async Task<List<Organization>> GetAllOrganizations()
        {
            return await _mediator.Send(new GetAllOrganizationQuery());
        }
    }
}
