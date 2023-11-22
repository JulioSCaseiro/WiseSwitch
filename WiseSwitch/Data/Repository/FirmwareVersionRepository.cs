using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data.Entities;
using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data.Repository
{
    public class FirmwareVersionRepository : IFirmwareVersionRepository
    {
        private readonly DbSet<FirmwareVersion> _firmwareVersionDbSet;

        public FirmwareVersionRepository(DataContext context)
        {
            _firmwareVersionDbSet = context.FirmwareVersions;
        }


        public async Task CreateAsync(FirmwareVersion firmwareVersion)
        {
            await _firmwareVersionDbSet.AddAsync(firmwareVersion);
        }

        public async Task DeleteAsync(int id)
        {
            _firmwareVersionDbSet.Remove(await _firmwareVersionDbSet.FindAsync(id));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _firmwareVersionDbSet.AnyAsync(firmwareVersion => firmwareVersion.Id == id);
        }

        public async Task<IEnumerable<FirmwareVersion>> GetAllOrderByVersionAsync()
        {
            return await _firmwareVersionDbSet
                .AsNoTracking()
                .OrderBy(firmwareVersion => firmwareVersion.Version)
                .ToListAsync();
        }

        public async Task<FirmwareVersion> GetAsNoTrackingByIdAsync(int id)
        {
            return await _firmwareVersionDbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SelectListItem>> GetComboFirmwareVersionsAsync()
        {
            return await _firmwareVersionDbSet
                .Select(firmwareVersion => new SelectListItem
                {
                    Text = firmwareVersion.Version,
                    Value = firmwareVersion.Id.ToString(),
                })
                .ToListAsync();
        }

        public void Update(FirmwareVersion firmwareVersion)
        {
            _firmwareVersionDbSet.Update(firmwareVersion);
        }
    }
}
