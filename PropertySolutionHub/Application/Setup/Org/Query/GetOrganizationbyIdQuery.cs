using MediatR;
using PropertySolutionHub.Api.Dto.Estate;
using PropertySolutionHub.Application.Estate.PropertyComponent.Command;
using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Setup;

namespace PropertySolutionHub.Application.Estate.Org.Query
{
    public class GetOrganizationByIdQuery : IRequest<Organization>
    {
        public int Id { get; set; }
    }
}
