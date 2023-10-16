using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Domain.Entities.Lease;
using PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Command;
using PropertySolutionHub.Application.Users.LeaseAgreementComponent.Command;
using PropertySolutionHub.Application.Estate.LeaseAgreementComponent.Query;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;

namespace LeaseAgreementSolutionHub.Api.Controllers.Lease
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class LeaseAgreementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaseAgreementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddLeaseAgreement(LeaseAgreement property)
        {
            return await _mediator.Send(new CreateLeaseAgreementCommand { LeaseAgreement = property });
        }

        [HttpPost("[action]")]
        public async Task<LeaseAgreement> EditLeaseAgreement(LeaseAgreement property)
        {
            return await _mediator.Send(new UpdateLeaseAgreementCommand { LeaseAgreement = property });
        }

        [HttpGet("GetAll")]
        public async Task<List<LeaseAgreement>> GetAllLeaseAgreements()
        {
            return await _mediator.Send(new GetAllLeaseAgreementsQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<LeaseAgreement> GetLeaseAgreementById(int id)
        {
            return await _mediator.Send(new GetLeaseAgreementByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteLeaseAgreement(int id)
        {
            return await _mediator.Send(new DeleteLeaseAgreementCommand { Id = id });
        }
    }
}
