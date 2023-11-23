﻿using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class SwitchModelRepository : ISwitchModelRepository
    {
        public DbSet<SwitchModel> _switchModelDbSet;

        public SwitchModelRepository(DataContext context)
        {
            _switchModelDbSet = context.SwitchModels;
        }


        public async Task CreateAsync(SwitchModel switchModel)
        {
            await _switchModelDbSet.AddAsync(switchModel);
        }

        public async Task DeleteAsync(int id)
        {
            _switchModelDbSet.Remove(await _switchModelDbSet.FindAsync(id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _switchModelDbSet.AnyAsync(brand => brand.Id == id);
        }

        public async Task<IEnumerable<SwitchModel>> GetAllOrderByModelNameAsync()
        {
            return await _switchModelDbSet
                .AsNoTracking()
                .OrderBy(switchModel => switchModel.ModelName)
                .ToListAsync();
        }

        public async Task<SwitchModel> GetAsNoTrackingByIdAsync(int id)
        {
            return await _switchModelDbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<string>> GetSwitchModelsNamesOfFirmwareVersionAsync(int firmwareVersionId)
        {
            return await _switchModelDbSet
                .Where(switchModel => switchModel.DefaultFirmwareVersionId == firmwareVersionId)
                .Select(switchModel => switchModel.ModelName)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSwitchModelsNamesOfProductSeriesAsync(int productSeriesId)
        {
            return await _switchModelDbSet
                .Where(switchModel => switchModel.ProductSeriesId == productSeriesId)
                .Select(switchModel => switchModel.ModelName)
                .ToListAsync();
        }

        public void Update(SwitchModel switchModel)
        {
            _switchModelDbSet.Update(switchModel);
        }
    }
}