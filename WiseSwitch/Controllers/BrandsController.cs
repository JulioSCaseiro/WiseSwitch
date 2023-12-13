using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Data.Entities;
using WiseSwitch.Services;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.Brand;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class BrandsController : Controller
    {
        private readonly ApiService _apiService;
        private readonly DataService _dataService;

        public BrandsController(ApiService apiService, DataService dataService)
        {
            _apiService = apiService;
            _dataService = dataService;
        }


        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetAllBrandsOrderByName, null));
        }


        // GET: Brands/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetDisplayBrand, id));
        }


        // GET: Brands/Create
        public async Task<IActionResult> Create(int manufacturerId)
        {
            var model = manufacturerId < 1 ? null : new CreateBrandViewModel { ManufacturerId = manufacturerId };

            return await ViewCreate(model);
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBrandViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalidOnCreate(model);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateBrand, model);

                return Success($"Brand created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Brand.");
            return await ViewCreate(model);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
                return IdIsNotValid("Brand");

            var model = await _dataService.GetDataAsync(DataOperations.GetModelBrand, id);
            if (model == null)
                return NotFound("Brand");

            if (model is Brand brand)
            {
                return await ViewInputAsync(brand);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            if (model.Id < 1)
                return IdIsNotValid("Brand");

            if (!ModelState.IsValid)
                return await ModelStateInvalidOnEdit(model);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateBrand, model);
                return Success($"Brand updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Brand.");
            return await ViewEdit(model);
        }


        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return IdIsNotValid("Brand");

            var model = await _dataService.GetDataAsync(DataOperations.GetDisplayBrand, id);
            if (model == null)
                return NotFound("Brand");

            return View(model);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1)
                return IdIsNotValid("Brand");

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteBrand, id);

                return Success("Brand deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Brand.");
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

        private async Task<IActionResult> ModelStateInvalidOnCreate(CreateBrandViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Brand was not accepted. Review the input and try again.");

            return await ViewCreate(model);
        }

        private async Task<IActionResult> ModelStateInvalidOnEdit(EditBrandViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Brand was not accepted. Review the input and try again.");

            return await ViewEdit(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ViewCreate(CreateBrandViewModel model)
        {
            ViewBag.ComboManufacturers = await _dataUnit.Manufacturers.GetComboManufacturersAsync();
            return View(nameof(Create), model);
        }

        private async Task<IActionResult> ViewEdit(EditBrandViewModel model)
        {
            ViewBag.ComboManufacturers = await _dataUnit.Manufacturers.GetComboManufacturersAsync();
            return View(nameof(Edit), model);
        }

        #endregion private helper methods
    }
}
