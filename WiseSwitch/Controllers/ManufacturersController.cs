using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.Manufacturer;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ManufacturersController : Controller
    {
        private readonly ApiService _apiService;
        private readonly DataService _dataService;

        public ManufacturersController(ApiService apiService, DataService dataService)
        {
            _apiService = apiService;
            _dataService = dataService;
        }


        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetAllManufacturersOrderByName, null));
        }


        // GET: Manufacturers/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetDisplayManufacturer, id));
        }


        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateManufacturerViewModel model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalidOnCreate(model);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateManufacturer, model);

                return Success($"Manufacturer created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Manufacturer.");
            return View(model);
        }


        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
                return IdIsNotValid("Manufacturer");

            var model = await _dataService.GetDataAsync(DataOperations.GetModelManufacturer, id);
            if (model == null)
                return NotFound("Manufacturer");

            if (model is EditManufacturerViewModel manufacture)
            {
                return await ViewEdit(manufacture);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditManufacturerViewModel model)
        {
            if (model.Id < 1)
                return IdIsNotValid("Manufacturer");

            if (!ModelState.IsValid)
                return ModelStateInvalidOnEdit(model);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateManufacturer, model);
                return Success($"Manufacturer updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Manufacturer.");
            return View(model);
        }


        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return IdIsNotValid("Manufacturer");

            var model = await _dataService.GetDataAsync(DataOperations.GetDisplayManufacturer, id);
            if (model == null)
                return NotFound("Manufacturer");

            return View(model);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1)
                return IdIsNotValid("Manufacturer");

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteManufacturer, id);

                return Success("Manufacturer deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Manufacturer.");
            return await Delete(id);
        }


        #region private helper methods

        private IActionResult IdIsNotValid(string entityName)
        {
            TempData["LayoutMessageWarning"] = $"Cannot find {entityName} because ID is not valid.";
            return RedirectToAction(nameof(Index));
        }

        private IActionResult NotFound(string entityName)
        {
            var model = new NotFoundViewModel
            {
                Title = $"{entityName} not found",
                Message = $"The {entityName} you're looking for was not found."
            };

            return View(nameof(NotFound), model);
        }

        private IActionResult ModelStateInvalidOnCreate(CreateManufacturerViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Manufacturer was not accepted. Review the input and try again.");

            return View(model);
        }

        private IActionResult ModelStateInvalidOnEdit(EditManufacturerViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Manufacturer was not accepted. Review the input and try again.");

            return View(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ViewCreate(CreateManufacturerViewModel model)
        {
            ViewBag.ComboManufacturers = await _dataService.GetDataAsync(DataOperations.GetComboManufacturers, null);
            return View(nameof(Create), model);
        }

        private async Task<IActionResult> ViewEdit(EditManufacturerViewModel model)
        {
            ViewBag.ComboManufacturers = await _dataService.GetDataAsync(DataOperations.GetComboManufacturers, null);
            return View(nameof(Edit), model);
        }

        #endregion private helper methods
    }
}
