using WiseSwitch.Data.Entities;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<string>> GetBrandNamesOfManufacturerAsync(int manufacturerId);
    }
}