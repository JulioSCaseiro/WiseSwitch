using Microsoft.AspNetCore.Identity;

namespace WiseSwitch.Data.Identity
{
    public interface IIdentityManager
    {
        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> SetRoleOfUserAsync(AppUser user, string roleName);
        Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent);
        Task SignOutAsync();
        Task<bool> UserExistsAsync(string userName);
    }
}