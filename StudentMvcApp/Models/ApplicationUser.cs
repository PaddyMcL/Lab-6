using Microsoft.AspNetCore.Identity;

namespace StudentMvcApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public int? Age { get; set; } // Make nullable
    }

}
