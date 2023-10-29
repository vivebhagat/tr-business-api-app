using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Api.Dto.Auth;
using PropertySolutionHub.Application.Users.CustomerComponent.Command;
using PropertySolutionHub.Application.Users.CustomerComponent.Query;
using PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Auth;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return new BadRequestObjectResult("Invalid input parameters.");

            LoginRequestDto loginRequestDto = new() { UserName = username, PassWord = password };

            var result = await _mediator.Send(new CustomerLoginCommand { LoginRequestDto = loginRequestDto });

            return result != null? Ok(new { result }): Unauthorized();
        }

        [HttpGet("[action]/{id}"), Authorize]
        public async Task<IActionResult> Logout(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new BadRequestObjectResult("Invalid input parameter.");

            bool result = await _mediator.Send(new CustomerLogoutCommand { UserId = id });

            return result? Ok(new { result }): Problem("Error while logging out the requested session.");
        }


        [HttpPost("[action]"), Authorize]
        public async Task<IActionResult> RoleSelect([FromForm] string role, [FromForm] string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(refreshToken))
                return new BadRequestObjectResult("Invalid input parameters.");

            RoleSelectionRequestDto roleSelectionRequestDto = new() { Role = role, RefreshToken = refreshToken }; 

            var result = await _mediator.Send(new CustomerRoleCommand { RoleSelectionRequestDto = roleSelectionRequestDto });

            return result != null? Ok(new { result }): Unauthorized();
        }

        [HttpGet("[action]"), Authorize]
        public async Task<List<CustomerToRoleMap>> GetCustomerRoles(string userId)
        {
            return await _mediator.Send(new GetCustomerRolesByUserIdQuery { UserId = userId });
        }

        [HttpPost("[action]")]
        public async Task<int> AddCustomer(Customer customer)
        {
            string controllerName = ControllerContext.ActionDescriptor.ControllerName;
            return await _mediator.Send(new CreateCustomerCommand { Customer = customer, DataRoute = controllerName });
        }

        [HttpPost("[action]")]
        public async Task<Customer> EditCustomer(Customer customer)
        {
            return await _mediator.Send(new UpdateCustomerCommand { Customer = customer });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteCustomer(int id)
        {
            return await _mediator.Send(new DeleteCustomerCommand { Id = id });
        }

        [HttpGet("GetAll")]
        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _mediator.Send(new GetAllCustomersQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _mediator.Send(new GetCustomerByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<Customer> GetCustomerByUserId(string id)
        {
            return await _mediator.Send(new GetCustomerByUserIdQuery { UserId = id });
        }
    }
}
