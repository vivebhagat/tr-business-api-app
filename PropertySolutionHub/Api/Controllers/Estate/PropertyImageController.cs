using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertyImageSolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class PropertyImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddPropertyImage([FromForm] string modelstring, [FromForm] IFormFile file)
        {

            CreatePropertyImageCommand @object = null;

            try
            {
                @object = JsonConvert.DeserializeObject<CreatePropertyImageCommand>(modelstring);
            }
            catch (JsonSerializationException exe)
            {
                return new BadRequestObjectResult("Request is invalid.");
            }

            if (@object == null)
            {
                return new BadRequestObjectResult("Request is invalid or empty.");
            }

            return await _mediator.Send(new CreatePropertyImageCommand { PropertyImage = file, Name = @object.Name, Id = @object.Id });
        }

        [HttpPost("[action]")]
        public async Task<PropertyImage> EditPropertyImage(PropertyImage property)
        {
            return await _mediator.Send(new UpdatePropertyImageCommand { PropertyImage = property });
        }

        [HttpGet("GetAll")]
        public async Task<List<PropertyImage>> GetAllProperties()
        {
            return await _mediator.Send(new GetAllPropertyImagesQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<List<PropertyImage>> GetPropertyImageList(int Id)
        {
            return await _mediator.Send(new GetPropertyImageByPropertyIdQuery { Id = Id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeletePropertyImage(int Id)
        {
            return await _mediator.Send(new DeletePropertyImageCommand { Id = Id });
        }
    }
}
