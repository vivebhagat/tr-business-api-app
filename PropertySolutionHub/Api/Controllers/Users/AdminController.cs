﻿ using MediatR;
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
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Invalid input parameters.");

            LoginRequestDto loginRequestDto = new() { UserName = username, PassWord = password };

            AuthResponse result = await _mediator.Send(new AdminLoginCommand { LoginRequestDto = loginRequestDto });

            if (result != null)
                return Ok(new { result });
            else
                return Unauthorized();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("Invalid input parameters.");

            bool result = await _mediator.Send(new AdminLogoutCommand { UserId = userId });

            if (result)
                return Ok(new { result });
            else
                return Unauthorized();
        }


        [HttpPost("[action]"), Authorize]
        public async Task<IActionResult> RoleSelect([FromForm] string role, [FromForm] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            RoleSelectionRequestDto roleSelectionRequestDto = new() { Role = role, RefreshToken = refreshToken };

            var result = await _mediator.Send(new AdminRoleCommand { RoleSelectionRequestDto = roleSelectionRequestDto });

            if (result != null)
                return Ok(new { result });
            else
                return Unauthorized();
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
