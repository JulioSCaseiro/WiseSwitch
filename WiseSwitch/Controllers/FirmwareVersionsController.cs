using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.FirmwareVersion;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class FirmwareVersionsController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public FirmwareVersionsController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: FirmwareVersions
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.FirmwareVersions.GetAllOrderByVersionAsync());
        }


        // GET: FirmwareVersions/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 1) return IdIsNotValid("Firmware Version");

            var model = await _dataUnit.FirmwareVersions.GetDisplayViewModelAsync(id);
            if (model == null) return NotFound("Firmware Version");

            return View(model);
        }


        // GET: FirmwareVersions/Create
        public IActionResult Create()
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
                await _dataUnit.FirmwareVersions.CreateAsync(new FirmwareVersion
                {
                    Version = model.Version,
                    LaunchDate = model.LaunchDate,
                });
                await _dataUnit.SaveChangesAsync();

                return Success($"Firmware Version created: {model.Version}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Firmware Version.");
            return View(model);
        }


        // GET: FirmwareVersions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid("Firmware Version");

            var model = await _dataUnit.FirmwareVersions.GetEditViewModelAsync(id);
            if (model == null) return NotFound("Firmware Version");

            return View(model);
        }

        // POST: FirmwareVersions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFirmwareVersionViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid("Firmware Version");

            if (!ModelState.IsValid)
                return ModelStateInvalidOnEdit(model);

            try
            {
                var firmwareVersion = await _dataUnit.FirmwareVersions.GetForUpdateAsync(model.Id);

                firmwareVersion.Version = model.Version;
                firmwareVersion.LaunchDate = model.LaunchDate;

                await _dataUnit.SaveChangesAsync();

                return Success($"Firmware Version updated: {model.Version}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.FirmwareVersions.ExistsAsync(model.Id))
                {
                    return NotFound("Firmware Version");
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Firmware Version.");
            return View(model);
        }


        // GET: FirmwareVersions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid("Firmware Version");

            var model = await _dataUnit.FirmwareVersions.GetDisplayViewModelAsync(id);
            if (model == null) return NotFound("Firmware Version");

            return View(model);
        }

        // POST: FirmwareVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid("Firmware Version");

            try
            {
                await _dataUnit.FirmwareVersions.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Firmware Version deleted.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_SwitchModels_FirmwareVersions_DefaultFirmwareVersionId"))
                    {
                        return RedirectToAction(nameof(Delete), id);
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Firmware Version.");
            return View(id);
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

        #endregion private helper methods
    }
}
