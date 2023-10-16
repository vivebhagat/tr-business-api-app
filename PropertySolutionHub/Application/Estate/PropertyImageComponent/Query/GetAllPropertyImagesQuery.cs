using MediatR;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Estate.PropertyImageComponent.Query
{
    public class GetAllPropertyImagesQuery : IRequest<List<PropertyImage>>
    {
    }
}
