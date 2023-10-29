using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.Org.Query;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
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
        public async Task<ActionResult<Organization>> EditOrganization([FromForm] string modelString, [FromForm] IFormFile file)
        {
            UpdateOrganizationCommand @object = null;
            try
            {
                @object = JsonConvert.DeserializeObject<UpdateOrganizationCommand>(modelString);
            }
            catch (JsonSerializationException exe)
            {
                return new BadRequestObjectResult("Request is invalid.");
            }

            if (@object == null)
            {
                return new BadRequestObjectResult("Request is invalid or empty.");
            }

            return await _mediator.Send(new UpdateOrganizationCommand { Organization = @object.Organization, OrgImage = file });
        }

        [HttpGet("[action]/{id}")]
        public async Task<Organization> GetOrganizationById(int Id)
        {
            return await _mediator.Send(new GetOrganizationByIdQuery { Id = Id});
        }

        [HttpGet("[action]")]
        public async Task<List<Organization>> GetAllOrganizations()
        {
            return await _mediator.Send(new GetAllOrganizationQuery());
        }
    }
}
