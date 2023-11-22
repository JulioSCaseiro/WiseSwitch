using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IProductSeriesRepository
    {
        Task CreateAsync(ProductSeries productSeries);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string productSeriesName);
        IQueryable<ProductSeries> GetAllAsNoTracking();
        Task<IEnumerable<ProductSeries>> GetAllOrderByName();
        Task<ProductSeries> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<string>> GetProductSeriesNamesOfProductLineAsync(int productLineId);
        void Update(ProductSeries productSeries);
    }
}