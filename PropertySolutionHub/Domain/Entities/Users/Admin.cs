using System.ComponentModel.DataAnnotations;
using PropertySolutionHub.Domain.Entities.Shared;

namespace PropertySolutionHub.Domain.Entities.Users
{
    public class Admin : IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? ArchiveDate { get; set; }
    }
}
