using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.FirmwareVersion;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class FirmwareVersionsController : AppController
    {
        private readonly DataService _dataService;

        protected override async Task GetInputCombos()
        {
            // This entity doesn't need Input Combos.
            await Task.CompletedTask;
        }

        public FirmwareVersionsController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: FirmwareVersions
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetAsync<IEnumerable<IndexRowFirmwareVersionViewModel>>(DataOperations.GetFirmwareVersionsOrderByVersion, null));
        }


        // GET: FirmwareVersions/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataService.GetAsync<DisplayFirmwareVersionViewModel>(DataOperations.GetFirmwareVersionDisplay, id);
            if (model == null) return NotFound(EntityNames.FirmwareVersion);

            return View(model);
        }


        // GET: FirmwareVersions/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInput(null);
        }

        // POST: FirmwareVersions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFirmwareVersionViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Brand);

            try
            {
                await _dataService.CreateAsync(DataOperations.CreateFirmwareVersion, model);

                return Success($"Firmware Version created: {model.Version}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Firmware Version.");
            return await ViewInput(model);
        }


        // GET: FirmwareVersions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            var model = await _dataService.GetAsync<EditFirmwareVersionViewModel>(DataOperations.GetFirmwareVersionEditModel, id);
            if (model == null) return NotFound(EntityNames.FirmwareVersion);

            return await ViewInput(model);
        }

        // POST: FirmwareVersions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFirmwareVersionViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.FirmwareVersion);

            try
            {
                await _dataService.UpdateAsync(DataOperations.UpdateFirmwareVersion, model);

                return Success($"Firmware Version updated: {model.Version}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Firmware Version.");
            return await ViewInput(model);
        }


        // GET: FirmwareVersions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            var model = await _dataService.GetAsync<DisplayFirmwareVersionViewModel>(DataOperations.GetFirmwareVersionDisplay, id);
            if (model == null) return NotFound(EntityNames.FirmwareVersion);

            return View(model);
        }

        // POST: FirmwareVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            try
            {
                await _dataService.DeleteAsync(DataOperations.DeleteFirmwareVersion, id);

                return Success("Firmware Version deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Firmware Version.");
            return await Delete(id);
        }
    }
}
