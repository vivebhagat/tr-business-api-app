using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Application.QueryModels.User;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;
using PropertySolutionHub.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.External
{

    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class BusinessUserExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessUserExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<BusinessUser> GetBusinessUserById(int id)
        {
            return await _mediator.Send(new GetBusinessUserDetailsByIdQuery { Id = id});
        }
    }
}