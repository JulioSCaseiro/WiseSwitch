using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

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

        public async Task<IEnumerable<Manufacturer>> GetAllOrderByName()
        {
            return await _manufacturerDbSet
                .AsNoTracking()
                .OrderBy(x => x.Name)
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
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .OrderBy(x => x.Text)
                .ToListAsync();
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
