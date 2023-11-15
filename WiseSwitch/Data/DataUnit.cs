using WiseSwitch.Data.Repository;

namespace WiseSwitch.Data
{
    public class DataUnit : IDataUnit
    {
        private readonly DataContext _context;

        public DataUnit(
            DataContext context,
            IUserRepository userRepository)
        {
            _context = context;
            Users = userRepository;
        }

        public IUserRepository Users { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
