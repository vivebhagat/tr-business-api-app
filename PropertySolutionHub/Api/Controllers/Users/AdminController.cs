 using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Application.Users.AdminComponent.Command;
using PropertySolutionHub.Application.Users.AdminComponent.Query;
using PropertySolutionHub.Application.Users.AdminToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<AuthResponse>> Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return new BadRequestObjectResult("Invalid input parameters.");

            LoginRequestDto loginRequestDto = new() { UserName = username, PassWord = password };

            AuthResponse result = await _mediator.Send(new AdminLoginCommand { LoginRequestDto = loginRequestDto });

            return result != null? Ok(new { result }): Unauthorized();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> Logout(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return new BadRequestObjectResult("Invalid input parameter.");

            bool result = await _mediator.Send(new AdminLogoutCommand { UserId = userId });

            return result ? Ok(new { result }): Problem("Error while logging out the requested session.");
        }


        [HttpPost("[action]"), Authorize]
        public async Task<ActionResult<RoleAuthResponse>> RoleSelect([FromForm] string role, [FromForm] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(refreshToken))
                return new BadRequestObjectResult("Invalid input parameters.");

            RoleSelectionRequestDto roleSelectionRequestDto = new() { Role = role, RefreshToken = refreshToken };

            var result = await _mediator.Send(new AdminRoleCommand { RoleSelectionRequestDto = roleSelectionRequestDto });

            return result != null ? Ok(new { result }): Unauthorized();
        }

        [HttpGet("[action]"), Authorize]
        public async Task<List<AdminToRoleMap>> GetRoles(string userId)
        {
            return await _mediator.Send(new GetAdminRolesByUserIdQuery { UserId = userId });
        }

        [HttpPost("[action]")]
        public async Task<int> AddAdmin(Admin admin)
        {
            string controllerName = ControllerContext.ActionDescriptor.ControllerName;
            return await _mediator.Send(new CreateAdminCommand { Admin = admin, DataRoute = controllerName });
        }

        [HttpPost("[action]")]
        public async Task<Admin> EditAdmin(Admin admin)
        {
            return await _mediator.Send(new UpdateAdminCommand { Admin = admin });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteAdmin(int id)
        {
            return await _mediator.Send(new DeleteAdminCommand { Id = id });
        }

        [HttpGet("GetAll")]
        public async Task<List<Admin>> GetAllAdmins()
        {
            return await _mediator.Send(new GetAllAdminsQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<Admin> GetAdminById(int id)
        {
            return await _mediator.Send(new GetAdminByIdQuery { Id = id });
        }
    }
}
