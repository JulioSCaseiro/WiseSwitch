using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels.Entities.SwitchModel;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface ISwitchModelRepository
    {
        Task CreateAsync(SwitchModel switchModel);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(string modelName);
        Task<IEnumerable<IndexRowSwitchModelViewModel>> GetAllOrderByModelNameAsync();
        Task<SwitchModel> GetAsNoTrackingByIdAsync(int id);
        Task<int> GetBrandIdAsync(int switchModelId);
        Task<SwitchModel> GetForUpdateAsync(int id);
        Task<InputSwitchModelViewModel> GetInputViewModelAsync(int id);
        Task<IEnumerable<string>> GetSwitchModelsNamesOfFirmwareVersionAsync(int firmwareVersionId);
        Task<IEnumerable<string>> GetSwitchModelsNamesOfProductSeriesAsync(int productSeriesId);
        void Update(SwitchModel switchModel);
    }
}