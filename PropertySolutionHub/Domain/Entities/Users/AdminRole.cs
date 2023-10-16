using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Domain.Entities.Users
{
    public class AdminRole : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}

