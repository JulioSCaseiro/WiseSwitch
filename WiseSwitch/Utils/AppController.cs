using Microsoft.AspNetCore.Mvc;
using WiseSwitch.ViewModels;

namespace WiseSwitch.Utils
{
    public abstract class AppController : Controller
    {
        protected abstract Task GetInputCombos();


        protected IActionResult IdIsNotValid(string entityName)
        {
            TempData["LayoutMessageWarning"] = $"The given ID for {EntityNames.Spaced(entityName)} is not valid.";
            return RedirectToAction("Index");
        }

        protected async Task<IActionResult> ModelStateInvalid(IInputViewModel model, string entityName)
        {
            ModelState.AddModelError(
                string.Empty,
                $"The input for the {EntityNames.Spaced(entityName)} was not accepted. Review the input and try again.");

            Response.StatusCode = StatusCodes.Status400BadRequest;

            return await ViewInput(model);
        }

        protected IActionResult NotFound(string entityName)
        {
            var model = new NotFoundViewModel
            {
                Title = $"{EntityNames.Spaced(entityName)} not found",
                Message = $"The {EntityNames.Spaced(entityName)} you're looking for was not found."
            };

            Response.StatusCode = StatusCodes.Status404NotFound;

            return View(nameof(NotFound), model);
        }

        protected IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        protected async Task<IActionResult> ViewInput(IInputViewModel model)
        {
            await GetInputCombos();
            return View(model);
        }
    }
}
