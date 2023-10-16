using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Application.QueryModels.User
{
    public class GetBusinessuser
    {
        public BusinessUser BusinessUser { get; set; }
        public List<BusinessUserToRoleMap> BusinessUserToRoleMapList { get; set; }
    }
}
