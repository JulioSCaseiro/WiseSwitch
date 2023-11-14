using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Data;
using WiseSwitch.Data.Identity;
using WiseSwitch.Exceptions;
using WiseSwitch.Exceptions.UsersControllerExceptions;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.User;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public UsersController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: UsersController
        public async Task<IActionResult> Index()
        {
            var users = await _dataUnit.Users
                .GetAllOrderByPropertiesAsync(
                    nameof(AppUser.Role),
                    nameof(AppUser.UserName));

            return View(users);
        }


        // GET: UsersController/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInputAsync(nameof(Create), null);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            // Id is not needed when creating User.
            ModelState.Remove(nameof(model.Id));

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, nameof(Create));

            try
            {
                var user = new AppUser { UserName = model.UserName.Trim(), Role = model.Role };

                var createUserInRole = await _dataUnit.Users.CreateInRoleAsync(user, model.Password, model.Role);
                if (createUserInRole.Succeeded)
                {
                    TempData["LayoutSuccessMessage"] = $"User created: {user.UserName}, {user.Role}.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (createUserInRole.Errors.Any(error => error.Code == "InvalidUserName"))
                    {
                        ModelState.AddModelError(string.Empty, "Username is not valid.");
                        return await ViewInputAsync(nameof(Edit), model);
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create User.");
            return await ViewInputAsync(nameof(Create), model);
        }


        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _dataUnit.Users.GetUserViewModelAsync(id);
            if (model == null) return UserNotFound();

            return await ViewInputAsync(nameof(Edit), model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model, bool newPassword)
        {
            // Remove Password from ModelState if it's not meant to change.
            if (!newPassword) ModelState.Remove(nameof(model.Password));

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, nameof(Edit));

            try
            {
                // -- Update whatever's been changed,
                // throw Exception if anything fails. --

                var user = await _dataUnit.Users.FindByIdAsync(model.Id);
                if (user == null) return UserNotFound();

                // Update UserName.
                if (user.UserName != model.UserName.Trim())
                {
                    user.UserName = model.UserName.Trim();
                }

                // Update Role.
                if (user.Role != model.Role)
                {
                    var setRole = await _dataUnit.Users.SetRoleAsync(user, model.Role);
                    if (setRole.Succeeded)
                    {
                        user.Role = model.Role;
                    }
                    else throw new UserRoleSetException();
                }

                // Save changes in database.
                var updateUser = await _dataUnit.Users.UpdateAsync(user);
                if (updateUser.Succeeded) { }
                else throw new UserUpdateException();

                // Update Password.
                if (newPassword)
                {
                    var setNewPassword = await _dataUnit.Users.SetNewPasswordAsync(user, model.Password);
                    if (setNewPassword.Succeeded) { }
                    else throw new NewPasswordSetException();
                }

                // Success.
                TempData["LayoutSuccessMessage"] = $"User updated: {user.UserName}, {user.Role}.";
                return RedirectToAction(nameof(Index));
            }
            catch (UsersControllerException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return await ViewInputAsync(nameof(Edit), model);
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update User.");
            return await ViewInputAsync(nameof(Edit), model);
        }


        // GET: UsersController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _dataUnit.Users.GetUserViewModelAsync(id);
            if (model == null) return UserNotFound();

            return View(model);
        }

        // POST: UsersController/Delete/5
        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var deleteUser = await _dataUnit.Users.DeleteAsync(id);
                if (deleteUser.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (deleteUser.Errors.Any(error => error.Code == "UserNotFound"))
                    {
                        return UserNotFound();
                    }
                }
            }
            catch { }

            TempData["LayoutMessageError"] = "Could not delete User.";
            return RedirectToAction(nameof(Index));
        }

        #region private helper methods

        private async Task<IActionResult> ModelStateInvalid(UserViewModel model, string viewName)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the User was not accepted. Review the input and try again.");

            return await ViewInputAsync(viewName, model);
        }

        private IActionResult UserNotFound()
        {
            var model = new NotFoundViewModel
            {
                Title = "User not found",
                Message = "The user you're looking for was not found."
            };
            return View(nameof(NotFound), model);
        }

        private async Task<IActionResult> ViewInputAsync(string viewName, UserViewModel? model)
        {
            ViewBag.ComboRoles = await _dataUnit.Users.GetComboRolesAsync();

            return View(viewName, model);
        }

        #endregion private helper methods
    }
}
