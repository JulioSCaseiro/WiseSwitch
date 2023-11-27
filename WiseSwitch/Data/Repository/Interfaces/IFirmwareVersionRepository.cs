using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IFirmwareVersionRepository
    {
        Task CreateAsync(FirmwareVersion firmwareVersion);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string version);
        Task<IEnumerable<FirmwareVersion>> GetAllOrderByVersionAsync();
        Task<FirmwareVersion> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetComboFirmwareVersionsAsync();
        Task<int> GetIdFromVersionAsync(string version);
        void Update(FirmwareVersion firmwareVersion);
    }
}