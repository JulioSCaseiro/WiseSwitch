using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IManufacturerRepository
    {
        Task CreateAsync(Manufacturer manufacturer);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string manufacturerName);
        IQueryable<Manufacturer> GetAllAsNoTracking();
        Task<IEnumerable<Manufacturer>> GetAllOrderByName();
        Task<Manufacturer?> GetAsNoTrackingByIdAsync(int id);
        Task<Manufacturer?> GetIfDeletableAsync(int id);
        void Update(Manufacturer manufacturer);
    }
}