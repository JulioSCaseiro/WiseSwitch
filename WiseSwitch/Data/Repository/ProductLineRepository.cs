using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;
using WiseSwitch.ViewModels.Entities.ProductLine;

namespace WiseSwitch.Data.Repository
{
    public class ProductLineRepository : IProductLineRepository
    {
        private readonly DbSet<ProductLine> _productLineDbSet;

        public ProductLineRepository(DataContext context)
        {
            _productLineDbSet = context.ProductLines;
        }


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

        public async Task<IEnumerable<IndexRowProductLineViewModel>> GetAllOrderByName()
        {
            return await _productLineDbSet
                .AsNoTracking()
                .OrderBy(productLine => productLine.Name)
                .Select(productLine => new IndexRowProductLineViewModel
                {
                    Id = productLine.Id,
                    Name = productLine.Name,
                    BrandName = productLine.Brand.Name,
                })
                .ToListAsync();
        }

        public async Task<ProductLine> GetAsNoTrackingByIdAsync(int id)
        {
            return await _productLineDbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(productLine => productLine.Id == id);
        }

        public async Task<int> GetBrandIdAsync(int id)
        {
            return await _productLineDbSet
                .Where(productLine => productLine.Id == id)
                .Select(productLine => productLine.BrandId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboProductLinesAsync()
        {
            return await _productLineDbSet
                .Select(productLine => new SelectListItem
                {
                    Text = productLine.Name,
                    Value = productLine.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetComboProductLinesOfBrandAsync(int brandId)
        {
            return await _productLineDbSet
                .Where(productLine => productLine.BrandId == brandId)
                .Select(productLine => new SelectListItem
                {
                    Text = productLine.Name,
                    Value = productLine.Id.ToString()
                })
                .ToListAsync();
        }

        public async Task<DisplayProductLineViewModel> GetDisplayViewModelAsync(int id)
        {
            return await _productLineDbSet
                .Where(productLine => productLine.Id == id)
                .Select(productLine => new DisplayProductLineViewModel
                {
                    Id = productLine.Id,
                    Name = productLine.Name,
                    BrandName = productLine.Brand.Name,
                    ProductSeriesNames = productLine.ProductSeries.Select(productSeries => productSeries.Name)
                })
                .SingleOrDefaultAsync();
        }

        public async Task<EditProductLineViewModel> GetEditViewModelAsync(int id)
        {
            return await _productLineDbSet
                .Where(productLine => productLine.Id == id)
                .Select(productLine => new EditProductLineViewModel
                {
                    Id = productLine.Id,
                    Name = productLine.Name,
                    BrandId = productLine.BrandId,
                })
                .SingleOrDefaultAsync();
        }

        public async Task<ProductLine> GetForUpdateAsync(int id)
        {
            return await _productLineDbSet.FindAsync(id);
        }

        public async Task<int> GetIdFromNameAsync(string name)
        {
            return await _productLineDbSet
                .Where(productLine => productLine.Name == name)
                .Select(productLine => productLine.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetProductLinesNamesOfBrandAsync(int brandId)
        {
            return await _productLineDbSet
                .Where(productLine => productLine.BrandId == brandId)
                .Select(productLine => productLine.Name)
                .ToListAsync();
        }

        public void Update(ProductLine productLine)
        {
            _productLineDbSet.Update(productLine);
        }
    }
}
