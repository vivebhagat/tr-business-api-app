using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Command
{
    public class UpdatePropertyImageCommand : IRequest<PropertyImage>
    {
        public PropertyImage PropertyImage { get; set; }

    }
}
