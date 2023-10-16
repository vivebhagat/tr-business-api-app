using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddProperty(CreatePropertyCommand @object)
        {
            return await _mediator.Send(new CreatePropertyCommand { Property = @object.Property });
        }

        [HttpPost("[action]")]
        public async Task<Property> EditProperty([FromForm] string modelString, [FromForm] IFormFile file)
        {
            UpdatePropertyCommand @object = JsonConvert.DeserializeObject<UpdatePropertyCommand>(modelString);

            if (@object == null)
            {
                throw new Exception("Invalid input parameters.");
            }

            var res =  await _mediator.Send(new UpdatePropertyCommand { Property = @object.Property, PropertyImage = file });

            return res;
        }

        [HttpGet("[action]")]
        public async Task<List<Property>> GetAllProperties()
        {
            return await _mediator.Send(new GetAllPropertiesQuery());
        }

        [HttpGet("[action]")]
        public async Task<List<Property>> GetAllFeaturedProperties()
        {
            return await _mediator.Send(new GetAllFeaturedPropertiesQuery());
        }


        [HttpGet("[action]/{id}")]
        public async Task<CreatePropertyCommand> GetPropertyById(int id)
        {
            return await _mediator.Send(new GetPropertyByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteProperty(int id)
        {
            return await _mediator.Send(new DeletePropertyCommand { Id = id });
        }


        [HttpGet("[action]")]
        public IEnumerable<Tuple<int, string>> GetPropertyType()
        {
            return Enum.GetValues(typeof(PropertyType)).Cast<PropertyType>().Select(value => new Tuple<int, string>((int)value, Enum.GetName(typeof(PropertyStatus), value)));
        }
         
        [HttpGet("[action]")]
        public IEnumerable<Tuple<int, string>> GetPropertyStatus()
        {
            return Enum.GetValues(typeof(PropertyStatus)).Cast<PropertyStatus>().Select(value => new Tuple<int, string>((int)value, Enum.GetName(typeof(PropertyStatus), value)));
        }
    }
}
