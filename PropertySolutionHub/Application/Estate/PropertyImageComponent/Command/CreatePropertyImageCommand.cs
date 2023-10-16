using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Command
{
    public class CreatePropertyImageCommand : IRequest<int>
    {
        public IFormFile PropertyImage { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
