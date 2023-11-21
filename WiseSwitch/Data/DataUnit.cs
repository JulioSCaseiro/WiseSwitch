using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data
{
    public class DataUnit : IDataUnit
    {
        private readonly DataContext _context;

        public DataUnit(
            DataContext context,
            IBrandRepository brandRepository,
            IManufacturerRepository manufacturerRepository,
            IProductLineRepository productLineRepository,
            IProductSeriesRepository productSeriesRepository,
            IUserRepository userRepository
            )
        {
            _context = context;

            Brands = brandRepository;
            Manufacturers = manufacturerRepository;
            ProductLines = productLineRepository;
            ProductSeries = productSeriesRepository;
            Users = userRepository;
        }

        public IBrandRepository Brands { get; }
        public IManufacturerRepository Manufacturers { get; }
        public IProductLineRepository ProductLines { get; }
        public IProductSeriesRepository ProductSeries { get; }
        public IUserRepository Users { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
