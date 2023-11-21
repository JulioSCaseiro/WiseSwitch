using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DbSet<Brand> _brandDbSet;

        public BrandRepository(DataContext context)
        {
            _brandDbSet = context.Brands;
        }


        public async Task CreateAsync(Brand brand)
        {
            await _brandDbSet.AddAsync(brand);
        }

        public async Task DeleteAsync(int id)
        {
            _brandDbSet.Remove(await _brandDbSet.FindAsync(id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _brandDbSet.AnyAsync(brand => brand.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _brandDbSet.AnyAsync(brand => brand.Name == name);
        }

        public async Task<IEnumerable<Brand>> GetAllOrderByNameAsync()
        {
            return await _brandDbSet
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Brand> GetAsNoTrackingByIdAsync(int id)
        {
            return await _brandDbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<string>> GetBrandNamesOfManufacturerAsync(int manufacturerId)
        {
            return await _brandDbSet
                .Where(brand => brand.ManufacturerId == manufacturerId)
                .Select(brand => brand.Name)
                .ToListAsync();
        }

        public void Update(Brand brand)
        {
            _brandDbSet.Update(brand);
        }
    }
}
