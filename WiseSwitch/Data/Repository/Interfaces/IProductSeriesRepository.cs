using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Dtos;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels.Entities.ProductSeries;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IProductSeriesRepository
    {
        Task CreateAsync(ProductSeries productSeries);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string productSeriesName);
        IQueryable<ProductSeries> GetAllAsNoTracking();
        Task<IEnumerable<IndexRowProductSeriesViewModel>> GetAllOrderByName();
        Task<ProductSeries> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboProductSeriesAsync();
        Task<IEnumerable<SelectListItem>> GetComboProductSeriesOfProductLineAsync(int productLineId);
        Task<DisplayProductSeriesViewModel> GetDisplayViewModelAsync(int id);
        Task<EditProductSeriesViewModel> GetEditViewModelAsync(int id);
        Task<ProductSeries> GetForUpdateAsync(int id);
        Task<int> GetIdFromNameAsync(string name);
        Task<ProductSeriesDependencyChainIds> GetIdsOfDependencyChainAsync(int id);
        Task<int> GetProductLineIdAsync(int id);
        Task<IEnumerable<string>> GetProductSeriesNamesOfProductLineAsync(int productLineId);
        void Update(ProductSeries productSeries);
    }
}