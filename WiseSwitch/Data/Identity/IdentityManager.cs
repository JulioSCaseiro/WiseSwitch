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

        public IQueryable<AppUser> Users => _userManager.Users;
        public IQueryable<IdentityRole> Roles => _roleManager.Roles;

        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "The user was not found."
                });
            }

            return await _userManager.DeleteAsync(user);
        }

        public async Task<AppUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> SetPasswordAsync(AppUser user, string newPassword)
        {
            var removeCurrentPassword = await _userManager.RemovePasswordAsync(user);
            if (removeCurrentPassword.Succeeded)
            {
                return await _userManager.AddPasswordAsync(user, newPassword);
            }
            else
            {
                return removeCurrentPassword;
            }
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

        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            return await _userManager.Users.AnyAsync(user => user.UserName == userName);
        }
    }
}
