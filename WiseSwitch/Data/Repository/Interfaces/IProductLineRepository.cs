using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IProductLineRepository
    {
        Task CreateAsync(ProductLine productLine);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string productLineName);
        IQueryable<ProductLine> GetAllAsNoTracking();
        Task<IEnumerable<ProductLine>> GetAllOrderByName();
        Task<ProductLine> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<string>> GetProductLinesNamesOfBrandAsync(int brandId);
        void Update(ProductLine productLine);
    }
}