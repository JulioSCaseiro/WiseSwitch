using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;
using WiseSwitch.ViewModels.Entities.ProductSeries;

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

        public async Task<IEnumerable<IndexRowProductSeriesViewModel>> GetAllOrderByName()
        {
            return await _productSeriesDbSet
                .AsNoTracking()
                .OrderBy(productSeries => productSeries.Name)
                .Select(productSeries => new IndexRowProductSeriesViewModel
                {
                    Id = productSeries.Id,
                    Name = productSeries.Name,
                    ProductLineName = productSeries.ProductLine.Name,
                    BrandName = productSeries.ProductLine.Brand.Name,
                })
                .ToListAsync();
        }

        public async Task<ProductSeries> GetAsNoTrackingByIdAsync(int id)
        {
            return await _productSeriesDbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(productSeries => productSeries.Id == id);
        }

        public async Task<IEnumerable<SelectListItem>> GetComboProductSeriesAsync()
        {
            return await _productSeriesDbSet
                .Select(productSeries => new SelectListItem
                {
                    Text = productSeries.Name,
                    Value = productSeries.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboProductSeriesOfProductLineAsync(int productLineId)
        {
            return await _productSeriesDbSet
                .Where(productSeries => productSeries.ProductLineId == productLineId)
                .Select(productSeries => new SelectListItem
                {
                    Text = productSeries.Name,
                    Value = productSeries.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<ProductSeries> GetForUpdateAsync(int id)
        {
            return await _productSeriesDbSet.FindAsync(id);
        }

        public async Task<int> GetIdFromNameAsync(string name)
        {
            return await _productSeriesDbSet
                .Where(productSeries => productSeries.Name == name)
                .Select(productSeries => productSeries.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<InputProductSeriesViewModel> GetInputViewModelAsync(int id)
        {
            return await _productSeriesDbSet
                .Where(productSeries => productSeries.Id == id)
                .Select(productSeries => new InputProductSeriesViewModel
                {
                    Id = productSeries.Id,
                    Name = productSeries.Name,
                    ProductLineId = productSeries.ProductLineId,
                    BrandId = productSeries.ProductLine.BrandId,
                })
                .SingleOrDefaultAsync();
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
