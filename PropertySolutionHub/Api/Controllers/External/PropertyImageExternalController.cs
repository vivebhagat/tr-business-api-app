using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Command;
using PropertySolutionHub.Application.Estate.PropertyImageComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.External
{
    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class PropertyImageExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyImageExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<List<PropertyImage>> GetPropertyImages(int Id)
        {
            return await _mediator.Send(new GetAllPropertyImagesByRemoteProeprtyIdQuery { Id = Id });
        }
    }
}
