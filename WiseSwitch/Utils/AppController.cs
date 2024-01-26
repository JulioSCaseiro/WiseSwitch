using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services.Data;
using WiseSwitch.ViewModels;

namespace WiseSwitch.Utils
{
    public abstract class AppController : Controller
    {
        protected abstract Task GetInputCombos();


        protected IActionResult ApiFailed()
        {
            TempData["LayoutMessageError"] = "API service failed.";

            Response.StatusCode = StatusCodes.Status500InternalServerError;

            return View("Error");
        }

        public IActionResult CustomError(string message, int statusCode, object model = null)
        {
            TempData["LayoutMessageError"] = message;

            Response.StatusCode = statusCode;

            if (model != null)
            {
                return View(model);
            }

            return View("Error");
        }

        public IActionResult GenericError()
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return View("Error");
        }

        protected IActionResult IdIsNotValid(string entityName)
        {
            TempData["LayoutMessageWarning"] = $"The given ID for {EntityNames.Spaced(entityName)} is not valid.";
            return RedirectToAction("Index");
        }

        protected IActionResult ManageDeleteResponse(DataResponse dataResponse)
        {
            if (dataResponse.IsSuccess)
            {
                if (dataResponse.Message != null)
                {
                    return Success(dataResponse.Message);
                }
                else
                {
                    return Success("The requested action was successfully concluded.");
                }
            }

            return ResponseIsNotSuccessful(dataResponse);
        }

        protected IActionResult ManageGetDataResponse<T>(DataResponse dataResponse)
        {
            if (dataResponse.IsSuccess)
            {
                if (dataResponse.Result is T model)
                {
                    return View(model);
                }
                else
                {
                    return CustomError("Could not cast model from response.", StatusCodes.Status500InternalServerError);
                }
            }

            return ResponseIsNotSuccessful(dataResponse);
        }

        protected IActionResult ManageInputResponse(DataResponse dataResponse)
        {
            if (dataResponse.IsSuccess)
            {
                if (dataResponse.Message != null)
                {
                    return Success(dataResponse.Message);
                }
                else
                {
                    return Success("The requested action was successfully concluded.");
                }
            }

            return ResponseIsNotSuccessful(dataResponse);
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

        protected IActionResult ResponseIsNotSuccessful(DataResponse dataResponse)
        {
            if (dataResponse.Message != null)
            {
                if (dataResponse.Message.Contains("API"))
                {
                    return ApiFailed();
                }

                // Error here on Delete failure.
                return CustomError(dataResponse.Message, StatusCodes.Status400BadRequest);
            }

            return GenericError();
        }

        public IActionResult ServerError(string message, object model = null)
        {
            return CustomError(message, StatusCodes.Status500InternalServerError);
        }

        protected IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction("Index");
        }

        protected async Task<IActionResult> ViewInput(IInputViewModel model)
        {
            await GetInputCombos();
            return View(model);
        }
    }
}
