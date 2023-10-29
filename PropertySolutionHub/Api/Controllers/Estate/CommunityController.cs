using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionHub.Application.Estate.CommunityComponent.Command;
using PropertySolutionHub.Application.Estate.CommunityComponent.Query;
using PropertySolutionHub.Application.Estate.CommunityComponent.Notification;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class CommunityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCommunity(CreateCommunityCommand @object)
        {
            var result = await _mediator.Send(new CreateCommunityCommand { Community = @object.Community });

            if (result == 0)
                return Problem("Error while creating the community.");

            await _mediator.Publish<CommunityCreatedEvent>(new CommunityCreatedEvent(@object.Community, result));

            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Community>> EditCommunity([FromForm] string modelString, [FromForm] IFormFile file)
        {
            UpdateCommunityCommand @object = null;
            try
            {
                @object = JsonConvert.DeserializeObject<UpdateCommunityCommand>(modelString);
            }
            catch (JsonSerializationException exe)
            {
                return new BadRequestObjectResult("Request is invalid.");
            }

            if (@object == null)
            {
                return new BadRequestObjectResult("Request is invaiid or empty.");
            }

            return await _mediator.Send(new UpdateCommunityCommand { Community = @object.Community, CommunityImage = file });
        }

        [HttpGet("[action]")]
        public async Task<List<Community>> GetAllProperties()
        {
            return await _mediator.Send(new GetAllCommunititesQuery());
        }

        [HttpGet("[action]")]
        public async Task<List<Community>> GetAllFeaturedProperties()
        {
            return await _mediator.Send(new GetAllFeaturedCommunitiesQuery());
        }


        [HttpGet("[action]/{id}")]
        public async Task<CreateCommunityCommand> GetCommunityById(int id)
        {
            return await _mediator.Send(new GetCommunityByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteCommunity(int id)
        {
            return await _mediator.Send(new DeleteCommunityCommand { Id = id });
        }
    }
}
