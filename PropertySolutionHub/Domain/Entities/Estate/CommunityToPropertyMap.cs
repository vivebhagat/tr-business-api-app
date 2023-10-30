using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Domain.Entities.Estate
{
    public class CommunityToPropertyMap : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Community Community { get; set; }
        public int CommunityId { get; set; }
        public virtual Property Property { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
