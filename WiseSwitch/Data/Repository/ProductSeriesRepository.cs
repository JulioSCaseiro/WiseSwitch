using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class ProductSeriesRepository : IProductSeriesRepository
    {
        private readonly DbSet<ProductSeries> _productSeriesDbSet;

        public ProductSeriesRepository(DataContext context)
        {
            _productSeriesDbSet = context.ProductSeries;
        }


        public async Task CreateAsync(ProductSeries productSeries)
        {
            await _productSeriesDbSet.AddAsync(productSeries);
        }

        public async Task DeleteAsync(int id)
        {
            _productSeriesDbSet.Remove(
                await _productSeriesDbSet
                    .SingleAsync(productSeries => productSeries.Id == id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _productSeriesDbSet.AnyAsync(productSeries => productSeries.Id == id);
        }

        public async Task<bool> ExistsAsync(string productSeriesName)
        {
            return await _productSeriesDbSet.AnyAsync(productSeries => productSeries.Name == productSeriesName);
        }

        public IQueryable<ProductSeries> GetAllAsNoTracking()
        {
            return _productSeriesDbSet.AsNoTracking();
        }

        public async Task<IEnumerable<ProductSeries>> GetAllOrderByName()
        {
            return await _productSeriesDbSet
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ProductSeries> GetAsNoTrackingByIdAsync(int id)
        {
            return await _productSeriesDbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(productSeries => productSeries.Id == id);
        }

        public async Task<IEnumerable<string>> GetProductSeriesNamesOfProductLineAsync(int productLineId)
        {
            return await _productSeriesDbSet
                .Where(productSeries => productSeries.ProductLineId == productLineId)
                .Select(productSeries => productSeries.Name)
                .ToListAsync();
        }

        public void Update(ProductSeries productSeries)
        {
            _productSeriesDbSet.Update(productSeries);
        }
    }
}
