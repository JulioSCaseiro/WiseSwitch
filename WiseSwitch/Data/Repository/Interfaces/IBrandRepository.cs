using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels.Entities.Brand;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IBrandRepository
    {
        Task CreateAsync(Brand brand);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string name);
        Task<IEnumerable<IndexRowBrandViewModel>> GetAllOrderByNameAsync();
        Task<Brand> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<string>> GetBrandNamesOfManufacturerAsync(int manufacturerId);
        Task<IEnumerable<SelectListItem>> GetComboBrandsAsync();
        Task<DisplayBrandViewModel> GetDisplayViewModelAsync(int id);
        Task<int> GetIdFromNameAsync(string brandName);
        void Update(Brand brand);
    }
}