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


        public async Task<Manufacturer> GetIfDeletableAsync(int id)
        {
            return await _manufacturerDbSet
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(x => !x.Brands.Any());
        }

        public async Task CreateAsync(Manufacturer manufacturer)
        {
            await _manufacturerDbSet.AddAsync(manufacturer);
        }

        public async Task DeleteAsync(int id)
        {
            _manufacturerDbSet.Remove(
                await _manufacturerDbSet
                    .SingleAsync(manufacturer => manufacturer.Id == id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _manufacturerDbSet.AnyAsync(manufacturer => manufacturer.Id == id);
        }

        public async Task<bool> ExistsAsync(string manufacturerName)
        {
            return await _manufacturerDbSet.AnyAsync(manufacturer => manufacturer.Name == manufacturerName);
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

        public void Update(Manufacturer manufacturer)
        {
            _manufacturerDbSet.Update(manufacturer);
        }
    }
}
