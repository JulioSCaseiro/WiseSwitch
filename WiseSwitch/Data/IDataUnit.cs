using WiseSwitch.Data.Repository.Interfaces;

namespace WiseSwitch.Data
{
    public interface IDataUnit
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}