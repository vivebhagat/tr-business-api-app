using MediatR;
using PropertySolutionHub.Api.Dto.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyComponent.Command
{
    public class CreatePropertyCommand : IRequest<int>
    {
        public PropertyDto Property { get; set; }
        public IFormFile PropertyImage { get; set; }
    }
}
