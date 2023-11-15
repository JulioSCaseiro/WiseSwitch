using Microsoft.AspNetCore.Identity;

namespace WiseSwitch.Data.Identity
{
    public class AppUser : IdentityUser
    {
        private string _userName;

        public override string UserName
        {
            get => _userName;
            set => _userName = value.Trim();
        }

        public string Role { get; set; }
    }
}
