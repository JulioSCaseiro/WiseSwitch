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
            return await ViewInputAsync(null);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            // Id is not needed when creating User.
            ModelState.Remove(nameof(model.Id));

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                var user = new AppUser { UserName = model.UserName.Trim(), Role = model.Role };

                var createUserInRole = await _dataUnit.Users.CreateInRoleAsync(user, model.Password, model.Role);
                if (createUserInRole.Succeeded)
                {
                    return Success($"User created: {user.UserName}, {user.Role}.");
                }
                else
                {
                    if (createUserInRole.Errors.Any(error => error.Code == "InvalidUserName"))
                    {
                        ModelState.AddModelError(string.Empty, "Username is not valid.");
                        return await ViewInputAsync(model);
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create User.");
            return await ViewInputAsync(model);
        }


        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!IsIdValid(id)) return IdIsNotValid();

            var model = await _dataUnit.Users.GetUserViewModelAsync(id);
            if (model == null) return UserNotFound();

            return await ViewInputAsync(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model, bool newPassword)
        {
            if (!IsIdValid(model.Id)) return IdIsNotValid();

            // Remove Password from ModelState if it's not meant to change.
            if (!newPassword) ModelState.Remove(nameof(model.Password));

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

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
                    // Try setting role.
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
                    // Try setting new password.
                    var setNewPassword = await _dataUnit.Users.SetNewPasswordAsync(user, model.Password);
                    if (setNewPassword.Succeeded) { }
                    else throw new NewPasswordSetException();
                }

                // Success.
                return Success($"User updated: {user.UserName}, {user.Role}.");
            }
            catch (UsersControllerException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return await ViewInputAsync(model);
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update User.");
            return await ViewInputAsync(model);
        }


        // GET: UsersController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (!IsIdValid(id)) return IdIsNotValid();

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
            if (!IsIdValid(id)) return IdIsNotValid();

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

        private IActionResult IdIsNotValid()
        {
            TempData["LayoutMessageWarning"] = "Could not find User because the given ID is not valid.";
            return RedirectToAction(nameof(Index));
        }

        private static bool IsIdValid(string id)
        {
            return Guid.TryParse(id, out Guid guid) && guid != Guid.Empty;
        }

        private async Task<IActionResult> ModelStateInvalid(UserViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the User was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
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

        private async Task<IActionResult> ViewInputAsync(UserViewModel model)
        {
            ViewBag.ComboRoles = await _dataUnit.Users.GetComboRolesAsync();

            return View(model);
        }

        #endregion private helper methods
    }
}
