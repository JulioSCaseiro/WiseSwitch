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
        //Task<ProductLine> GetIfDeletableAsync(int id);
        void Update(ProductLine productLine);
    }
}