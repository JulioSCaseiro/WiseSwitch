using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Identity;

namespace WiseSwitch.Data
{
    public class InitDb
    {
        private readonly DataContext _context;
        private readonly IIdentityManager _identityManager;

        public InitDb(DataContext context, IIdentityManager identityManager)
        {
            _context = context;
            _identityManager = identityManager;
        }


        public async Task SeedAsync()
        {
            await MigrateAsync();

            await SeedRolesAsync();
            await SeedUsersAsync();

            await SaveChangesAsync();
        }


        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task SeedRolesAsync()
        {
            var defaultRoles = new string[] { "Admin", "Technician", "Operator" };

            foreach (var roleName in defaultRoles)
            {
                if (!await _identityManager.RoleExistsAsync(roleName))
                {
                    await _identityManager.CreateRoleAsync(new IdentityRole(roleName));
                }
            }
        }

        public async Task SeedUsersAsync()
        {
            // Create users and assign role.
            
            var defaultUserNames = new string[] { "Admin", "Technician", "Operator" };

            foreach (var userName in defaultUserNames)
            {
                if (!await _identityManager.UserExistsAsync(userName))
                {
                    // New user.
                    var user = new AppUser { UserName = userName };

                    // Save user in database.
                    var createUser = await _identityManager.CreateUserAsync(user, userName);
                    if (createUser == null || !createUser.Succeeded)
                    {
                        throw new Exception($"Could not create user. {createUser?.Errors}");
                    }

                    // Add user to role.
                    var addUserToRole = await _identityManager.AddUserToRoleAsync(user, userName);
                    if (addUserToRole == null || !addUserToRole.Succeeded)
                    {
                        throw new Exception($"Could not add user to role. {addUserToRole?.Errors}");
                    }
                }
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
