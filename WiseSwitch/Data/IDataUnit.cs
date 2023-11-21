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

        Task<int> SaveChangesAsync();
    }
}