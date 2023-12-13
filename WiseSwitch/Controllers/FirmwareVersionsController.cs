using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Entities;
using WiseSwitch.Services;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.Brand;
using WiseSwitch.ViewModels.Entities.FirmwareVersion;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class FirmwareVersionsController : Controller
    {
        private readonly DataService _dataService;
        private readonly ApiService _apiService;

        public FirmwareVersionsController(DataService dataService, ApiService apiService)
        {
            _dataService = dataService;
            _apiService = apiService;

        }


        // GET: FirmwareVersions
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetAllFirmwareVersionsOrderByVersion, null));
        }


        // GET: FirmwareVersions/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetDisplayFirmwareVersion, id));
        }


        // GET: FirmwareVersions/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: FirmwareVersions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFirmwareVersionViewModel model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalidOnCreate(model);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateFirmwareVersion, model);

                return Success($"Firmware Version created: {model.Version}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Firmware Version.");
            return View(model);
        }


        // GET: FirmwareVersions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
                return IdIsNotValid("Firmware Version");


            var model = await _dataService.GetDataAsync(DataOperations.GetModelFirmwareVersion, id);
            if (model == null)
                return NotFound("Firmware Version");

            if (model is EditFirmwareVersionViewModel brand)
            {
                return await ViewEdit(brand);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: FirmwareVersions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFirmwareVersionViewModel model)
        {
            if (model.Id < 1)
                return IdIsNotValid("Firmware Version");


            if (!ModelState.IsValid)
                return ModelStateInvalidOnEdit(model);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateFirmwareVersion, model);

                return Success($"Firmware Version updated: {model.Version}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Firmware Version.");
            return View(model);
        }


        // GET: FirmwareVersions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                return IdIsNotValid("Firmware Version");

            var model = await _dataService.GetDataAsync(DataOperations.GetDisplayFirmwareVersion, id);
            if (model == null)
                return NotFound("Firmware Version");

            return View(model);
        }

        // POST: FirmwareVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1)
                return IdIsNotValid("Firmware Version");
            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteFirmwareVersion, id);

                return Success("Firmware Version deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Firmware Version.");
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

        private IActionResult ModelStateInvalidOnCreate(CreateFirmwareVersionViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Firmware Version was not accepted. Review the input and try again.");

            return View(model);
        }

        private IActionResult ModelStateInvalidOnEdit(EditFirmwareVersionViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Firmware Version was not accepted. Review the input and try again.");

            return View(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ViewCreate(CreateFirmwareVersionViewModel model)
        {
            //ViewBag.ComboManufacturers = await _dataUnit.Manufacturers.GetComboManufacturersAsync();
            return View(nameof(Create), model);
        }

        private async Task<IActionResult> ViewEdit(EditFirmwareVersionViewModel model)
        {
            //ViewBag.ComboManufacturers = await _dataUnit.Manufacturers.GetComboManufacturersAsync();
            return View(nameof(Edit), model);
        }

        #endregion private helper methods
    }
}
