namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface ISwitchModelRepository
    {
        Task<IEnumerable<string>> GetSwitchModelsNamesOfProductSeriesAsync(int productSeriesId);
    }
}