using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Query
{
    public class GetAllPropertyImagesByRemoteProeprtyIdQuery : IRequest<List<PropertyImage>>
    {
        public int Id { get; set; }
    }
}
