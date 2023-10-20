using MediatR;
using PropertySolutionHub.Domain.Entities.Setup;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.Users.CustomerToRoleMapComponent.Query
{
    public class GetAllOrganizationQuery : IRequest<List<Organization>>
    {
    }
}