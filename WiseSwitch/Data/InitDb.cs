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
        private readonly IDataUnit _dataUnit;

        public InitDb(
            IConfiguration configuration,
            DataContext context,
            IIdentityManager identityManager,
            IDataUnit dataUnit)
        {
            _configuration = configuration;
            _context = context;
            _identityManager = identityManager;
            _dataUnit = dataUnit;
        }


        public async Task SeedAsync()
        {
            await MigrateAsync();

            await SeedRolesAsync();
            await SeedUsersAsync();

            await SeedManufacturersAsync();
            await SeedBrandsAsync();
            await SeedProductLinesAsync();
            await SeedProductSeriesAsync();
            await SeedFirmwareVersionsAsync();

            await SaveChangesAsync();
        }


        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task SeedBrandsAsync()
        {
            var defaultBrands = _configuration["SeedDb:Brands:DefaultBrands"].Split(',');

            // Need to save changes to be able to get Manufacturers.
            await SaveChangesAsync();

            foreach (var brand in defaultBrands)
            {
                var name = _configuration[$"SeedDb:Brands:{brand}:Name"];
                var manufacturer = _configuration[$"SeedDb:Brands:{brand}:Manufacturer"];

                if (!await _dataUnit.Brands.ExistsAsync(brand))
                {
                    await _dataUnit.Brands.CreateAsync(new Entities.Brand
                    {
                        Name = name,
                        ManufacturerId = await _dataUnit.Manufacturers.GetIdFromNameAsync(manufacturer)
                    });
                }
            }
        }

        public async Task SeedFirmwareVersionsAsync()
        {
            var defaultFirmwareVersions = _configuration["SeedDb:FirmwareVersions:DefaultFirmwareVersions"].Split(',');

            foreach (var firmwareVersion in defaultFirmwareVersions)
            {
                var version = _configuration[$"SeedDb:FirmwareVersions:{firmwareVersion}:Version"];
                var launchDate = _configuration[$"SeedDb:FirmwareVersions:{firmwareVersion}:LaunchDate"];

                if (!await _dataUnit.FirmwareVersions.ExistsAsync(firmwareVersion))
                {
                    await _dataUnit.FirmwareVersions.CreateAsync(new Entities.FirmwareVersion
                    {
                        Version = version,
                        LaunchDate = string.IsNullOrEmpty(launchDate) ? null : DateTime.Parse(launchDate),
                    });
                }
            }
        }

        public async Task SeedManufacturersAsync()
        {
            var defaultManufacturers = _configuration["SeedDb:Manufacturers"].Split(',');

            foreach (var manufacturerName in defaultManufacturers)
            {
                if (!await _dataUnit.Manufacturers.ExistsAsync(manufacturerName))
                {
                    await _dataUnit.Manufacturers.CreateAsync(new Entities.Manufacturer { Name = manufacturerName });
                }
            }
        }

        public async Task SeedProductLinesAsync()
        {
            var defaultProductLines = _configuration["SeedDb:ProductLines:DefaultProductLines"].Split(',');

            // Need to save changes to be able to get Brands.
            await SaveChangesAsync();

            foreach (var productLine in defaultProductLines)
            {
                var name = _configuration[$"SeedDb:ProductLines:{productLine}:Name"];
                var brand = _configuration[$"SeedDb:ProductLines:{productLine}:Brand"];

                if (!await _dataUnit.ProductLines.ExistsAsync(productLine))
                {
                    await _dataUnit.ProductLines.CreateAsync(new Entities.ProductLine
                    {
                        Name = name,
                        BrandId = await _dataUnit.Brands.GetIdFromNameAsync(brand)
                    });
                }
            }
        }

        public async Task SeedProductSeriesAsync()
        {
            var defaultProductSeries = _configuration["SeedDb:ProductSeries:DefaultProductSeries"].Split(',');

            // Need to save changes to be able to get Product Lines.
            await SaveChangesAsync();

            foreach (var productSeries in defaultProductSeries)
            {
                var name = _configuration[$"SeedDb:ProductSeries:{productSeries}:Name"];
                var productLine = _configuration[$"SeedDb:ProductSeries:{productSeries}:ProductLine"];

                if (!await _dataUnit.ProductSeries.ExistsAsync(productSeries))
                {
                    await _dataUnit.ProductSeries.CreateAsync(new Entities.ProductSeries
                    {
                        Name = name,
                        ProductLineId = await _dataUnit.ProductLines.GetIdFromNameAsync(productLine)
                    });
                }
            }
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
