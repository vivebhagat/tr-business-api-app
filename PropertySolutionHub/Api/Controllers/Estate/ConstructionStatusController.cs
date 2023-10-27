using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Command;
using PropertySolutionHub.Application.Estate.ConstructionStatusComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;

namespace ConstructionStatusSolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class ConstructionStatusController
    {
        private readonly IMediator _mediator;

        public ConstructionStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddConstructionStatus(CreateConstructionStatusCommand @object)
        {
            return await _mediator.Send(new CreateConstructionStatusCommand { ConstructionStatus = @object.ConstructionStatus });
        }

        [HttpPost("[action]")]
        public async Task<ConstructionStatus> EditConstructionStatus(UpdateConstructionStatusCommand @object)
        {
            var res = await _mediator.Send(new UpdateConstructionStatusCommand { ConstructionStatus = @object.ConstructionStatus });
            return res;
        }

        [HttpGet("[action]")]
        public async Task<List<ConstructionStatus>> GetAllConstructionStatuss()
        {
            return await _mediator.Send(new GetAllConstructionStatusQuery());
        }


        [HttpGet("[action]/{id}")]
        public async Task<ConstructionStatus> GetConstructionStatusById(int id)
        {
            return await _mediator.Send(new GetConstructionStatusByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteConstructionStatus(int id)
        {
            return await _mediator.Send(new DeleteConstructionStatusCommand { Id = id });
        }

    }
}
