using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Data.Identity;
using WiseSwitch.ViewModels.Account;

namespace WiseSwitch.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityManager _identityManager;

        private bool IsUserAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public AccountController(IIdentityManager identityManager)
        {
            _identityManager = identityManager;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            if (IsUserAuthenticated) return Redirect("/Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return LoginModelStateInvalid(model);
            }

            // Try login.
            var signIn = await _identityManager.SignInAsync(model.UserName, model.Password, model.RememberMe);

            // If Login succeeded.
            if (signIn.Succeeded)
            {
                // Redirect to ReturnUrl or /Home.
                return Redirect((string)Request.Query["ReturnUrl"] ?? "/Home");
            }
            // If Login did not succeed.
            else
            {
                // Check user exists.
                if (!await _identityManager.UserExistsAsync(model.UserName))
                {
                    ModelState.AddModelError(string.Empty, "This user is not registered.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Could not login.");
                }

                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            if (IsUserAuthenticated)
                await _identityManager.SignOutAsync();

            return Redirect("/Home");
        }


        #region private helper methods

        private IActionResult LoginModelStateInvalid(LoginViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "Could not login - input was not accepted. Make sure you give a Username and a Password.");

            return View(model);
        }

        #endregion private helper methods
    }
}
