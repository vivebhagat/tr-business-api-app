using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Command;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class CommunityToPropertyMapController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityToPropertyMapController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteCommunityToPropertyMap(int id)
        {
            return await _mediator.Send(new DeleteCommunityToPropertyMapCommand { Id = id });
        }
    }
}
