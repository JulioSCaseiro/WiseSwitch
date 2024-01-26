using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;
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
            var getAll = await _dataService.GetAsync<IEnumerable<IndexRowFirmwareVersionViewModel>>(ApiUrls.GetAllFirmwareVersions);

            return ManageGetDataResponse<IEnumerable<IndexRowFirmwareVersionViewModel>>(getAll);
        }


        // GET: FirmwareVersions/5
        public async Task<IActionResult> Details(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            // Try get model.
            var getModel = await _dataService.GetAsync<DisplayFirmwareVersionViewModel>(ApiUrls.GetFirmwareVersionDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayFirmwareVersionViewModel>(getModel);
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
                return await ModelStateInvalid(model, EntityNames.FirmwareVersion);

            // Try create FirmwareVersion.
            var create = await _dataService.CreateAsync(ApiUrls.CreateFirmwareVersion, model);

            // Resolve response.
            return ManageInputResponse(create);
        }


        // GET: FirmwareVersions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            // Try to get model.
            var getModel = await _dataService.GetAsync<EditFirmwareVersionViewModel>(ApiUrls.GetFirmwareVersionEditModel, id);

            // Resolve response.
            return ManageGetDataResponse<EditFirmwareVersionViewModel>(getModel);
        }

        // POST: FirmwareVersions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFirmwareVersionViewModel model)
        {
            // Check given ID is valid.
            if (model.Id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.FirmwareVersion);

            // Try update FirmwareVersion.
            var update = await _dataService.UpdateAsync(ApiUrls.UpdateFirmwareVersion, model);

            // Resolve response.
            return ManageInputResponse(update);
        }


        // GET: FirmwareVersions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            // Try get Model.
            var getModel = await _dataService.GetAsync<DisplayFirmwareVersionViewModel>(ApiUrls.GetFirmwareVersionDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayFirmwareVersionViewModel>(getModel);
        }

        // POST: FirmwareVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.FirmwareVersion);

            // Try delete FirmwareVersion.
            var delete = await _dataService.DeleteAsync(ApiUrls.DeleteFirmwareVersion, id);

            // Resolve response.
            return ManageDeleteResponse(delete);
        }
    }
}
