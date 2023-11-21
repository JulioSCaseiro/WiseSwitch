using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Identity;
using WiseSwitch.ViewModels.Entities.User;

namespace WiseSwitch.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<IdentityResult> CreateInRoleAsync(AppUser user, string password, string roleName);
        Task<IdentityResult> DeleteAsync(string id);
        Task<AppUser> FindByIdAsync(string id);
        IQueryable<AppUser> GetAllAsNoTracking();
        Task<IEnumerable<UserViewModel>> GetAllOrderByPropertiesAsync(params string[] propertiesNames);
        Task<IEnumerable<SelectListItem>> GetComboRolesAsync();
        Task<UserViewModel> GetUserViewModelAsync(string id);
        Task<IdentityResult> SetNewPasswordAsync(AppUser user, string newPassword);
        Task<IdentityResult> SetRoleAsync(AppUser user, string roleName);
        Task<IdentityResult> UpdateAsync(AppUser user);
    }
}