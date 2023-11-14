using Microsoft.AspNetCore.Identity;

namespace WiseSwitch.Data.Identity
{
    public interface IIdentityManager
    {
        IQueryable<AppUser> Users { get; }
        IQueryable<IdentityRole> Roles { get; }

        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<AppUser> FindByIdAsync(string id);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> SetPasswordAsync(AppUser user, string newPassword);
        Task<IdentityResult> SetRoleOfUserAsync(AppUser user, string roleName);
        Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent);
        Task SignOutAsync();
        Task<IdentityResult> UpdateUserAsync(AppUser user);
        Task<bool> UserExistsAsync(string userName);
    }
}