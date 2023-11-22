using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class SwitchModelRepository : ISwitchModelRepository
    {
        public DbSet<SwitchModel> _switchModelsDbSet;

        public SwitchModelRepository(DataContext context)
        {
            _switchModelsDbSet = context.SwitchModels;
        }


        public async Task<IEnumerable<string>> GetSwitchModelsNamesOfProductSeriesAsync(int productSeriesId)
        {
            return await _switchModelsDbSet
                .Where(switchModel => switchModel.ProductSeriesId == productSeriesId)
                .Select(switchModel => switchModel.ModelName)
                .ToListAsync();
        }
    }
}
