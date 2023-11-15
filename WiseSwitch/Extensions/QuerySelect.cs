using WiseSwitch.Data.Identity;
using WiseSwitch.ViewModels.Entities.User;

namespace WiseSwitch.Extensions
{
    public static class QuerySelect
    {
        public static IQueryable<UserViewModel> SelectUserViewModel(this IQueryable<AppUser> query)
        {
            return query.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role
            });
        }
    }
}
