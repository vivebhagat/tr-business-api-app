using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Domain.Entities.Estate
{
    public class ContractRequest : IBaseEntity
    {
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public double ProposedPurchasePrice { get; set; }
        public ContractRequestStatus Status { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
