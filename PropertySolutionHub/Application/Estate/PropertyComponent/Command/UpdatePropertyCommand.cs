using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Command
{
    public class UpdatePropertyCommand : IRequest<Property>
    {
        public PropertyDto Property { get; set; }
        public IFormFile PropertyImage { get; set; }

    }
}
