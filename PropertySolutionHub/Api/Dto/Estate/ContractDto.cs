using PropertySolutionHub.Domain.Entities.Estate;
using PropertySolutionHub.Domain.Entities.Users;

namespace PropertySolutionHub.Api.Dto.Estate
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int CustomerId { get; set; }
        public decimal PurchasePrice { get; set; }
        public ContractStatus ContractStatus { get; set; }
        public DateTime SaleDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
