using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Application.Shared.BusinessUserToDomainKeyMapComponent.Query;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Application.Users.BusinessUserToDomainKeyMapComponent.Query;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Controllers.Standard
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class BusinessUserStandardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessUserStandardController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("[action]")]
        public async Task<List<BusinessUserToRoleMap>> GetBusinessUserRoles(string userId)
        {
            return await _mediator.Send(new GetBusinessUserToRoleMapByUserIdQuery { UserId = userId });
        }

        [HttpGet("[action]/{userId}")]
        public async Task<List<BaseApplicationUserToDomainKeyMap>> GetDomainKeysForUser(string userId)
        {
            return await _mediator.Send(new GetDomainKeyForUserQuery { Id = userId });
        }

        [HttpGet("[action]/{id}"), Authorize]
        public async Task<DomainDetail> SelectDomain(int id)
        {
            return await _mediator.Send(new ValidateDomainKeyQuery { Id = id });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<RoleAuthResponse>> RoleSelect([FromForm] string role, [FromForm] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(refreshToken))
                return new BadRequestObjectResult("Request is invalid.");

            RoleSelectionRequestDto roleSelectionRequestDto = new() { Role = role, RefreshToken = refreshToken };

            var result = await _mediator.Send(new BusinessUserRoleCommand { RoleSelectionRequestDto = roleSelectionRequestDto });

            return result != null? Ok(new { result }): Unauthorized();
        }
    }
}
