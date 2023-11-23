using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;
using WiseSwitch.ViewModels.Entities.Brand;

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

        public async Task<IEnumerable<IndexRowBrandViewModel>> GetAllOrderByNameAsync()
        {
            return await _brandDbSet
                .AsNoTracking()
                .OrderBy(brand => brand.Name)
                .Select(brand => new IndexRowBrandViewModel
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    ManufacturerName = brand.Manufacturer.Name,
                })
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

        public async Task<IEnumerable<SelectListItem>> GetComboBrandsAsync()
        {
            return await _brandDbSet
                .Select(brand => new SelectListItem
                {
                    Text = brand.Name,
                    Value = brand.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<int> GetIdFromNameAsync(string name)
        {
            return await _brandDbSet
                .Where(brand => brand.Name == name)
                .Select(brand => brand.Id)
                .SingleOrDefaultAsync();
        }

        public void Update(Brand brand)
        {
            _brandDbSet.Update(brand);
        }
    }
}
