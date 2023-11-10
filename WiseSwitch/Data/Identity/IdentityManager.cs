using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WiseSwitch.Data.Identity
{
    public class IdentityManager : IIdentityManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public IdentityManager(RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> SetRoleOfUserAsync(AppUser user, string roleName)
        {
            // Remove user from all roles user is in.
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

            // Add user to role.
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            return await _userManager.Users.AnyAsync(user => user.UserName == userName);
        }
    }
}
