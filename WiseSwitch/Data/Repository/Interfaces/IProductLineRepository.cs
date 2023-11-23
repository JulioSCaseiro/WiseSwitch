using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels.Entities.ProductLine;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IProductLineRepository
    {
        Task CreateAsync(ProductLine productLine);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string productLineName);
        IQueryable<ProductLine> GetAllAsNoTracking();
        Task<IEnumerable<IndexRowProductLineViewModel>> GetAllOrderByName();
        Task<ProductLine> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboProductLinesAsync();
        Task<int> GetIdFromNameAsync(string name);
        Task<IEnumerable<string>> GetProductLinesNamesOfBrandAsync(int brandId);
        void Update(ProductLine productLine);
    }
}