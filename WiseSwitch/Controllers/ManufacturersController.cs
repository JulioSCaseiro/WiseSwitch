using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.Manufacturer;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ManufacturersController : AppController
    {
        private readonly DataService _dataService;

        protected override async Task GetInputCombos()
        {
            // This entity doesn't need Input Combos.
            await Task.CompletedTask;
        }

        public ManufacturersController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            var getAll = await _dataService.GetAsync<IEnumerable<IndexRowManufacturerViewModel>>(ApiUrls.GetAllManufacturers);

            return ManageGetDataResponse<IEnumerable<IndexRowManufacturerViewModel>>(getAll);
        }


        // GET: Manufacturers/5
        public async Task<IActionResult> Details(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            // Try get model.
            var getModel = await _dataService.GetAsync<DisplayManufacturerViewModel>(ApiUrls.GetManufacturerDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayManufacturerViewModel>(getModel);
        }


        // GET: Manufacturers/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInput(null);
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateManufacturerViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Manufacturer);

            // Try create Manufacturer.
            var create = await _dataService.CreateAsync(ApiUrls.CreateManufacturer, model);

            // Resolve response.
            return ManageInputResponse(create);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            // Try to get model.
            var getModel = await _dataService.GetAsync<EditManufacturerViewModel>(ApiUrls.GetManufacturerEditModel, id);

            // Resolve response.
            return ManageGetDataResponse<EditManufacturerViewModel>(getModel);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditManufacturerViewModel model)
        {
            // Check given ID is valid.
            if (model.Id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Manufacturer);

            // Try update Manufacturer.
            var update = await _dataService.UpdateAsync(ApiUrls.UpdateManufacturer, model);

            // Resolve response.
            return ManageInputResponse(update);
        }


        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            // Try get Model.
            var getModel = await _dataService.GetAsync<DisplayManufacturerViewModel>(ApiUrls.GetManufacturerDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayManufacturerViewModel>(getModel);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            // Try delete Manufacturer.
            var delete = await _dataService.DeleteAsync(ApiUrls.DeleteManufacturer, id);

            // Resolve response.
            return ManageDeleteResponse(delete);
        }
    }
}
