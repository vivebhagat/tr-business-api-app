using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Api.Dto.Estate
{
    public class ContractRequestDto
    {
        public int Id { get; set; }
        public int RemoteId { get; set; }
        public int PropertyId { get; set; }
        public int CustomerId { get; set; }
        public decimal ProposedPurchasePrice { get; set; }
        public ContractRequestStatus Status { get; set; }
        public string Note { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
