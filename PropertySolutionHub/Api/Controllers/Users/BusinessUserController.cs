using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.QueryModels.User;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Query;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Logout(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("Invalid input parameters.");

            bool result = await _mediator.Send(new BusinessUserLogoutCommand { UserId = id });

            if (result)
                return Ok(new { result });
            else
                return Unauthorized();
        }

        [HttpPost("[action]")]
        public async Task<int> AddBusinessUser(CreateBusinessUserCommand @object)
        {
            return await _mediator.Send(new CreateBusinessUserCommand { BusinessUser = @object.BusinessUser, BusinessUserToRoleMapList = @object.BusinessUserToRoleMapList });
        }


        [HttpPost("[action]")]
        public async Task<BusinessUser> EditBusinessUser(UpdateBusinessUserCommand @object)
        {
            return await _mediator.Send(new UpdateBusinessUserCommand { BusinessUser = @object.BusinessUser, BusinessUserToRoleMapList = @object.BusinessUserToRoleMapList });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteBusinessUser(int id)
        {
            return await _mediator.Send(new DeleteBusinessUserCommand { Id = id });
        }

        [HttpGet("GetAll")]
        public async Task<List<BusinessUser>> GetAllBusinessUsers()
        {
            return await _mediator.Send(new GetAllBusinessUsersQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<GetBusinessuser> GetBusinessUserById(int id)
        {
            return await _mediator.Send(new GetBusinessUserByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<BusinessUser> GetBusinessUserByUserId(string id)
        {
            return await _mediator.Send(new GetBusinessUserByUserIdQuery { UserId = id });
        }

        [HttpGet("[action]")]
        public async Task<List<BusinessUserRole>> GetAllBusinessUserRoles()
        {
            return await _mediator.Send(new GetAllBusinessuserRolesQuery());
        }
    }
}
