using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Domain.Entities.Users
{
    public class BusinessUserToRoleMap : IBaseEntity
    {
        public int Id { get; set; }
        public virtual BusinessUser BusinessUser { get; set; }
        public int BusinessUserId { get; set; }
        public virtual BusinessUserRole Role { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
