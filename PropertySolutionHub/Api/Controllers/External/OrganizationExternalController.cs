using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.Org.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Setup.Org.Command;
using PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Setup;
using PropertySolutionHub.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.Setup.Org
{

    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class OrganizationExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<List<Organization>> GetAllOrganizations()
        {
            return await _mediator.Send(new GetAllOrganizationQuery());
        }
    }
}
