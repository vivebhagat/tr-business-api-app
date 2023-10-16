

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Application.Users.BusinessUserComponent.Command;
using PropertySolutionHub.Application.Users.BusinessUserRoleComponent.Command;
using PropertySolutionHub.Application.Users.BusinessUserToRoleMapComponent.Query;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessUserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddBusinessUserRole(CreateBusinessUserRoleCommand @object)
        {
            return await _mediator.Send(new CreateBusinessUserRoleCommand { BusinessUserRole = @object.BusinessUserRole });
        }


        [HttpPost("[action]")]
        public async Task<BusinessUserRole> EditBusinessUserRole(UpdateBusinessUserRoleCommand @object)
        {
            return await _mediator.Send(new UpdateBusinessUserRoleCommand { BusinessUserRole = @object.BusinessUserRole });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteBusinessUserRole(int id)
        {
            return await _mediator.Send(new DeleteBusinessUserRoleCommand { Id = id });
        }

        [HttpGet("[action]")]
        public async Task<List<BusinessUserRole>> GetAllBusinessUserRoles()
        {
            return await _mediator.Send(new GetAllBusinessuserRolesQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<BusinessUserRole> GetBusinessUserRoleById(int id)
        {
            return await _mediator.Send(new GetBusinessUserRolesByIdQuery { Id = id });
        }


    }
}
