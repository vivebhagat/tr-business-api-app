using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PropertySolutionCustomerPortal.Infrastructure.Attribute;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Command;
using PropertySolutionHub.Application.Estate.CommunityTypeComponent.Query;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Api.Controllers.Estate
{
    [Route("api/[controller]")]
    [ApiController, CustomAuthFilter]
    public class CommunityTypeController
    {
        private readonly IMediator _mediator;

        public CommunityTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<int> AddCommunityType(CreateCommunityTypeCommand @object)
        {
            return await _mediator.Send(new CreateCommunityTypeCommand { CommunityType = @object.CommunityType });
        }

        [HttpPost("[action]")]
        public async Task<CommunityType> EditCommunityType(UpdateCommunityTypeCommand @object)
        {
            var res = await _mediator.Send(new UpdateCommunityTypeCommand { CommunityType = @object.CommunityType});
            return res;
        }

        [HttpGet("[action]")]
        public async Task<List<CommunityType>> GetAllCommunityTypes()
        {
            return await _mediator.Send(new GetAllCommunityTypesQuery());
        }


        [HttpGet("[action]/{id}")]
        public async Task<CommunityType> GetCommunityTypeById(int id)
        {
            return await _mediator.Send(new GetCommunityTypeByIdQuery { Id = id });
        }

        [HttpGet("[action]/{id}")]
        public async Task<bool> DeleteCommunityType(int id)
        {
            return await _mediator.Send(new DeleteCommunityTypeCommand { Id = id });
        }

    }
}
