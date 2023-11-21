using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class ProductLineRepository : IProductLineRepository
    {
        private readonly DbSet<ProductLine> _productLineDbSet;

        public ProductLineRepository(DataContext context)
        {
            _productLineDbSet = context.ProductLines;
        }


        //public async Task<ProductLine> GetIfDeletableAsync(int id)
        //{
        //    return await _productLineDbSet
        //        .Where(x => x.Id == id)
        //        .FirstOrDefaultAsync(x => !x.ProductSeries.Any());
        //}

        public async Task CreateAsync(ProductLine productLine)
        {
            await _productLineDbSet.AddAsync(productLine);
        }

        public async Task DeleteAsync(int id)
        {
            _productLineDbSet.Remove(
                await _productLineDbSet
                    .SingleAsync(productLine => productLine.Id == id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _productLineDbSet.AnyAsync(productLine => productLine.Id == id);
        }

        public async Task<bool> ExistsAsync(string productLineName)
        {
            return await _productLineDbSet.AnyAsync(productLine => productLine.Name == productLineName);
        }

        public IQueryable<ProductLine> GetAllAsNoTracking()
        {
            return _productLineDbSet.AsNoTracking();
        }

        public async Task<IEnumerable<ProductLine>> GetAllOrderByName()
        {
            return await _productLineDbSet
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ProductLine> GetAsNoTrackingByIdAsync(int id)
        {
            return await _productLineDbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(productLine => productLine.Id == id);
        }

        public void Update(ProductLine productLine)
        {
            _productLineDbSet.Update(productLine);
        }
    }
}
