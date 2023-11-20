using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DbSet<Brand> _brands;

        public BrandRepository(DataContext context)
        {
            _brands = context.Brands;
        }


        public async Task<IEnumerable<string>> GetBrandNamesOfManufacturerAsync(int manufacturerId)
        {
            return await _brands
                .Where(brand => brand.ManufacturerId == manufacturerId)
                .Select(brand => brand.Name)
                .ToListAsync();
        }
    }
}
