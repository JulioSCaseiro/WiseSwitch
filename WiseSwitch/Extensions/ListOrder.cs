using WiseSwitch.ViewModels.Entities.User;

namespace WiseSwitch.Extensions
{
    public static class ListOrder
    {
        public static IEnumerable<UserViewModel> OrderByProperties(this IEnumerable<UserViewModel> list, params string[] properties)
        {
            // -- Order IEnumerable by given params --

            // Set role for ordering.
            var roleOrder = new List<string> { "Admin", "Technician", "Operator" };

            // Order by first param.
            var orderedList = properties[0] switch
            {
                nameof(UserViewModel.UserName) => list.OrderBy(model => model.UserName),
                nameof(UserViewModel.Role) => list.OrderBy(model => roleOrder.IndexOf(model.Role)),
                _ => throw new NotSupportedException("This property is not supported for ordering.")
            };

            // Order by subsequent params.
            for (var i = 1; i < properties.Length; i++)
            {
                orderedList = properties[i] switch
                {
                    nameof(UserViewModel.UserName) => orderedList.ThenBy(model => model.UserName),
                    nameof(UserViewModel.Role) => orderedList.ThenBy(model => roleOrder.IndexOf(model.Role)),
                    _ => throw new NotSupportedException("This property is not supported for ordering.")
                };
            }

            return orderedList;
        }
    }
}
