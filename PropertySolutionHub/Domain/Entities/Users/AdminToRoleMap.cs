using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Domain.Entities.Users
{
    public class AdminToRoleMap : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Admin Admin { get; set; }
        public int AdminId { get; set; }
        public virtual AdminRole Role { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
