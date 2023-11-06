using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Identity;

namespace WiseSwitch.Data
{
    public class InitDb
    {
        private readonly DataContext _context;
        private readonly IdentityManager _identityManager;

        public InitDb(DataContext context, IdentityManager identityManager)
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
            var defaultUserNames = new string[] { "Admin", "Technician", "Operator" };

            foreach (var userName in defaultUserNames)
            {
                if (!await _identityManager.UserExistsAsync(userName))
                {
                    var createUser = await _identityManager
                        .CreateUserAsync(
                            user: new AppUser { UserName = userName },
                            password: userName);

                    if (createUser == null || !createUser.Succeeded)
                    {
                        throw new Exception($"Could not create user. {createUser?.Errors}");
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
