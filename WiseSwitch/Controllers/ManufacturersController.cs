using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services;
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
            return View(await _dataService.GetDataAsync<IEnumerable<IndexRowManufacturerViewModel>>(DataOperations.GetAllManufacturersOrderByName, null));
        }


        // GET: Manufacturers/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataService.GetDataAsync<DisplayManufacturerViewModel>(DataOperations.GetDisplayManufacturer, id);
            if (model == null) return NotFound(EntityNames.Manufacturer);

            return View(model);
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

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateManufacturer, model);

                return Success($"Manufacturer created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Manufacturer.");
            return await ViewInput(model);
        }


        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            var model = await _dataService.GetDataAsync<EditManufacturerViewModel>(DataOperations.GetEditModelManufacturer, id);
            if (model == null) return NotFound(EntityNames.Manufacturer);

            return await ViewInput(model);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditManufacturerViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Manufacturer);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateManufacturer, model);

                return Success($"Manufacturer updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Manufacturer.");
            return await ViewInput(model);
        }


        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            var model = await _dataService.GetDataAsync<DisplayManufacturerViewModel>(DataOperations.GetDisplayManufacturer, id);
            if (model == null) return NotFound(EntityNames.Manufacturer);

            return View(model);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Manufacturer);

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteManufacturer, id);

                return Success("Manufacturer deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Manufacturer.");
            return await Delete(id);
        }
    }
}
