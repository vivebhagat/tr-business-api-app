using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Domain.Entities.Auth;

namespace PropertySolutionHub.Api.Controllers.Default
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUserDefaultController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessUserDefaultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<AuthResponse>> Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return new BadRequestObjectResult("Invalid input parameters.");

            LoginRequestDto loginRequestDto = new() { UserName = username, PassWord = password };

            AuthResponse result = await _mediator.Send(new BusinessUserLoginCommand { LoginRequestDto = loginRequestDto });

            return (result != null) ? Ok(new { result }): Unauthorized();
        }
    }
}
