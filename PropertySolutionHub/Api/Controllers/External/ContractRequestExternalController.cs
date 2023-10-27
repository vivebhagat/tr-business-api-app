using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Infrastructure.Attribute;

namespace PropertySolutionHub.Api.Controllers.External
{
    [Route("api/[controller]")]
    [ApiController, ExternalAuthFilter]
    public class ContractRequestExternalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractRequestExternalController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("[action]")]
        public async Task<int> AddContractRequest(CreateContractRequestCommand @object)
        {
            return await _mediator.Send(new CreateContractRequestCommand { ContractRequest = @object.ContractRequest });
        }

        [HttpPost("[action]")]
        public async Task<bool> WithdrawContractRequest(WithdrawContractRequestCommand @object)
        {
            return await _mediator.Send(new WithdrawContractRequestCommand { Id = @object.Id, DomainKey = @object.DomainKey });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteContractRequest(int id)
        {
            return await _mediator.Send(new DeleteContractRequestCommand { Id = id });
        }
    }
}