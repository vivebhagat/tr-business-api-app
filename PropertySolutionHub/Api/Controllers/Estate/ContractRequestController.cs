using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Command;
using PropertySolutionHub.Application.Estate.ContractRequestComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Util.Common;

namespace PropertySolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class ContractRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<ContractRequest> EditContractRequest(UpdateContractRequestCommand @object)
        {
            return await _mediator.Send(new UpdateContractRequestCommand { ContractRequest = @object.ContractRequest });
        }

        [HttpGet("GetAll")]
        public async Task<List<ContractRequest>> GetAllContractRequest()
        {
            return await _mediator.Send(new GetAllContractRequestsQuery());
        }

        [HttpGet("[action]/{id}")]
        public async Task<ContractRequest> GetContractRequestById(int id)
        {
            return await _mediator.Send(new GetContractRequestByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteContractRequest(int id)
        {
            return await _mediator.Send(new DeleteContractRequestCommand { Id = id });
        }

        [HttpGet("[action]")]
        public IEnumerable<Tuple<int, string>> GetContractRequestStatus()
        {
            return EnumHelper.GetEnumValues<ContractRequestStatus>();
        }
    }
}
