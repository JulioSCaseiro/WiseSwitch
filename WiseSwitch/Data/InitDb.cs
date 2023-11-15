using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Identity;

namespace WiseSwitch.Data
{
    public class InitDb
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IIdentityManager _identityManager;

        public InitDb(
            IConfiguration configuration,
            DataContext context,
            IIdentityManager identityManager)
        {
            _configuration = configuration;
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
            var defaultRoles = _configuration["SeedDb:Roles"].Split(',');

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
            // -- Create users and assign role --

            var defaultUsers = _configuration["SeedDb:Users:DefaultUsers"].Split(',');

            foreach (var defaultUser in defaultUsers)
            {
                if (!await _identityManager.UserExistsAsync(defaultUser))
                {
                    // Get necessary data from configuration file.
                    var userName = _configuration[$"SeedDb:Users:{defaultUser}:UserName"];
                    var password = _configuration[$"SeedDb:Users:{defaultUser}:Password"];
                    var roleName = _configuration[$"SeedDb:Users:{defaultUser}:Role"];

                    // New user.
                    var user = new AppUser { UserName = userName, Role = roleName };

                    // Save user in database.
                    var createUser = await _identityManager.CreateUserAsync(user, password);
                    if (createUser == null || !createUser.Succeeded)
                    {
                        throw new Exception($"Could not create user {userName}. {createUser?.Errors}");
                    }

                    // Add user to role.
                    var setRoleOfUser = await _identityManager.SetRoleOfUserAsync(user, roleName);
                    if (setRoleOfUser == null || !setRoleOfUser.Succeeded)
                    {
                        throw new Exception($"Could not set user's role: user {userName}, role {roleName}. {setRoleOfUser?.Errors}");
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
