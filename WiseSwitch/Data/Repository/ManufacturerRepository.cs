using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;
using WiseSwitch.ViewModels.Entities.Manufacturer;

namespace WiseSwitch.Data.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DbSet<Manufacturer> _manufacturerDbSet;

        public ManufacturerRepository(DataContext context)
        {
            _manufacturerDbSet = context.Manufacturers;
        }



        public async Task CreateAsync(Manufacturer manufacturer)
        {
            await _manufacturerDbSet.AddAsync(manufacturer);
        }

        public async Task DeleteAsync(int id)
        {
            _manufacturerDbSet.Remove(await _manufacturerDbSet.FindAsync(id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _manufacturerDbSet.AnyAsync(manufacturer => manufacturer.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _manufacturerDbSet.AnyAsync(manufacturer => manufacturer.Name == name);
        }

        public IQueryable<Manufacturer> GetAllAsNoTracking()
        {
            return _manufacturerDbSet.AsNoTracking();
        }

        public async Task<IEnumerable<IndexRowManufacturerViewModel>> GetAllOrderByNameAsync()
        {
            return await _manufacturerDbSet
                .OrderBy(manufacturer => manufacturer.Name)
                .Select(manufacturer => new IndexRowManufacturerViewModel
                {
                    Id = manufacturer.Id,
                    Name = manufacturer.Name,
                })
                .ToListAsync();
        }

        public async Task<Manufacturer> GetAsNoTrackingByIdAsync(int id)
        {
            return await _manufacturerDbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(manufacturer => manufacturer.Id == id);
        }

        public async Task<IEnumerable<SelectListItem>> GetComboManufacturersAsync()
        {
            return await _manufacturerDbSet
                .Select(manufacturer => new SelectListItem
                {
                    Text = manufacturer.Name,
                    Value = manufacturer.Id.ToString()
                })
                .OrderBy(manufacturer => manufacturer.Text)
                .ToListAsync();
        }

        public async Task<DisplayManufacturerViewModel> GetDisplayViewModelAsync(int id)
        {
            return await _manufacturerDbSet
                .Where(manufacturer => manufacturer.Id == id)
                .Select(manufacturer => new DisplayManufacturerViewModel
                {
                    Id = manufacturer.Id,
                    Name = manufacturer.Name,
                    BrandsNames = manufacturer.Brands.Select(brand => brand.Name)
                })
                .SingleOrDefaultAsync();
        }

        public async Task<EditManufacturerViewModel> GetEditViewModelAsync(int id)
        {
            return await _manufacturerDbSet
                .Where(manufacturer => manufacturer.Id == id)
                .Select(manufacturer => new EditManufacturerViewModel
                {
                    Id = manufacturer.Id,
                    Name = manufacturer.Name,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<Manufacturer> GetForUpdateAsync(int id)
        {
            return await _manufacturerDbSet.FindAsync(id);
        }

        public async Task<int> GetIdFromNameAsync(string name)
        {
            return await _manufacturerDbSet
                .Where(manufacturer => manufacturer.Name == name)
                .Select(manufacturer => manufacturer.Id)
                .SingleOrDefaultAsync();
        }

        public void Update(Manufacturer manufacturer)
        {
            _manufacturerDbSet.Update(manufacturer);
        }
    }
}
