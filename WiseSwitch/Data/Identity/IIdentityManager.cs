using Microsoft.AspNetCore.Identity;

namespace WiseSwitch.Data.Identity
{
    public interface IIdentityManager
    {
        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<bool> RoleExistsAsync(string roleName);
        Task<bool> UserExistsAsync(string userName);
    }
}