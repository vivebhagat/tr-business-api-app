using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Query
{
    public class GetPropertyByIdQuery : IRequest<CreatePropertyCommand>
    {
        public int Id { get; set; }
    }
}
