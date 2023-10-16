using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Query
{
    public class GetPropertyImageByPropertyIdQuery : IRequest<List<PropertyImage>>
    {
        public int Id { get; set; }
    }
}
