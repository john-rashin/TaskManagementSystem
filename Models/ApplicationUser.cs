using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}