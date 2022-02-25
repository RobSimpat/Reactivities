
using Microsoft.AspNetCore.Identity;

namespace Database.Models
{
    public class AppUser : IdentityUser

    {
        public string DisplayName { get; set; }
    }
}
