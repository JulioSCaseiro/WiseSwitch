using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data
{
    public interface IDataUnit
    {
        IBrandRepository Brands { get; }
        IManufacturerRepository Manufacturers { get; }
        IProductLineRepository ProductLines { get; }
        IProductSeriesRepository ProductSeries { get; }
        IUserRepository Users { get; }
        ISwitchModelRepository SwitchModels { get; }
        IFirmwareVersionRepository FirmwareVersions { get; }

        Task<int> SaveChangesAsync();
    }
}