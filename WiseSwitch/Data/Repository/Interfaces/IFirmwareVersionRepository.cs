using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IFirmwareVersionRepository
    {
        Task CreateAsync(FirmwareVersion firmwareVersion);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<FirmwareVersion>> GetAllOrderByVersionAsync();
        Task<FirmwareVersion> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboFirmwareVersionsAsync();
        void Update(FirmwareVersion firmwareVersion);
    }
}