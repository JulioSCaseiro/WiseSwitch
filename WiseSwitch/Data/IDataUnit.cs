using WiseSwitch.Data.Repository;

namespace WiseSwitch.Data
{
    public interface IDataUnit
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}