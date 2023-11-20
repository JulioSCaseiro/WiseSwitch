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
            IUserRepository userRepository)
        {
            _context = context;

            Brands = brandRepository;
            Manufacturers = manufacturerRepository;
            Users = userRepository;
        }

        public IBrandRepository Brands { get; }
        public IManufacturerRepository Manufacturers { get; }
        public IUserRepository Users { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
