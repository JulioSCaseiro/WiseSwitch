using Microsoft.AspNetCore.Identity;

namespace WiseSwitch.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
