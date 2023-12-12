using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels.Entities.Manufacturer;

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
        Task<Manufacturer> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboManufacturersAsync();
        Task<DisplayManufacturerViewModel> GetDisplayViewModelAsync(int id);
        Task<EditManufacturerViewModel> GetEditViewModelAsync(int id);
        Task<Manufacturer> GetForUpdateAsync(int id);
        Task<int> GetIdFromNameAsync(string name);
        void Update(Manufacturer manufacturer);
    }
}