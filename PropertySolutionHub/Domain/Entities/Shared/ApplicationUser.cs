using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertySolutionHub.Domain.Entities.Shared
{

    public class ApplicationUser : IdentityUser
    {
        public string DataRoute { get; set; }
        public string Password { get; set; }
    }
}
