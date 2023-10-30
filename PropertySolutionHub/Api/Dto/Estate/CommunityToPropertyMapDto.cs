using PropertySolutionHub.Domain.Entities.Estate;

namespace PropertySolutionHub.Api.Dto.Estate
{
    public class CommunityToPropertyMapDto
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyPrice { get; set; }
        public string PropertyType { get; set; }
        public string PropertyStatus { get; set; }
        public int CommunityId { get; set; }
        public int PropertyId { get; set; }
    }
}
