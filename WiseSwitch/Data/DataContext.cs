using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Identity;

namespace WiseSwitch.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<FirmwareVersion> FirmwareVersions { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ProductLine> ProductLines { get; set; }
        public DbSet<ProductSeries> ProductSeries { get; set; }
        public DbSet<SwitchModel> SwitchModels { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Product> Products { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Make all relationship's delete behavior Restrict, except for ownership

            var cascadeFKs = builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}
