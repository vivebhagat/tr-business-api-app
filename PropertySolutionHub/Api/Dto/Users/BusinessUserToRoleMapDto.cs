using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Dto.Users
{
    public class BusinessUserToRoleMapDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int BusinessUserId { get; set; }
    }
}
