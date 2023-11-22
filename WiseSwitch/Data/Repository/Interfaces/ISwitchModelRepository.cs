using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface ISwitchModelRepository
    {
        Task CreateAsync(SwitchModel switchModel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<SwitchModel>> GetAllOrderByModelNameAsync();
        Task<SwitchModel> GetAsNoTrackingByIdAsync(int id);
        Task<IEnumerable<string>> GetSwitchModelsNamesOfFirmwareVersionAsync(int firmwareVersionId);
        Task<IEnumerable<string>> GetSwitchModelsNamesOfProductSeriesAsync(int productSeriesId);
        void Update(SwitchModel switchModel);
    }
}