using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PropertySolutionHub.Domain.Entities.Shared
{
    public interface IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? ArchiveDate { get; set; }
    }
}
