using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IBrandRepository
    {
        Task CreateAsync(Brand brand);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<Brand>> GetAllOrderByNameAsync();
        Task<Brand> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<string>> GetBrandNamesOfManufacturerAsync(int manufacturerId);
        void Update(Brand brand);
    }
}