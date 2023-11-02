using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Application.Estate.CommunityToPropertyMapComponent.Query;
using PropertySolutionHub.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.External
{
    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class CommunityToPropertyMapExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommunityToPropertyMapExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<List<int>> GetPropertyIdListunderCommunity(int Id)
        {
            return await _mediator.Send(new GetPropertyIdListUnderCommunityQuery { Id = Id});
        }
    }
}