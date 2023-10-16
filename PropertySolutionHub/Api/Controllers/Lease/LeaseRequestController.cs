using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Application.Estate.LeaseRequestComponent.Command;
using PropertySolutionHub.Application.Users.LeaseRequestComponent.Command;
using PropertySolutionHub.Application.Estate.LeaseRequestComponent.Query;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;

namespace LeaseRequestSolutionHub.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class LeaseRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaseRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddLeaseRequest(CreateLeaseRequestCommand @obejct)
        {
            return await _mediator.Send(new CreateLeaseRequestCommand { LeaseRequest = @obejct.LeaseRequest });
        }

        [HttpPost("[action]")]
        public async Task<LeaseRequest> EditLeaseRequest(LeaseRequest @object)
        {
            return await _mediator.Send(new UpdateLeaseRequestCommand { LeaseRequest = @object });
        }

        [HttpGet("GetAll")]
        public async Task<List<LeaseRequest>> GetAllLeaseRequests()
        {
            return await _mediator.Send(new GetAllLeaseRequestsQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<LeaseRequest> GetLeaseRequestById(int id)
        {
            return await _mediator.Send(new GetLeaseRequestByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteLeaseRequest(int id)
        {
            return await _mediator.Send(new DeleteLeaseRequestCommand { Id = id });
        }
    }
}
