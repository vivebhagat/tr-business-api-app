using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Domain.Entities.Lease
{
    public class LeaseAgreement : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Property Property { get; set; }
        public int PropertyId { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public int LeaseTermMonths { get; set; }
        public double Amount { get; set; }
        public bool IsRenewable { get; set; }
        public DateTime? RenewedDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsTerminated { get; set; }
        public string TerminationReason { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
