using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.Brand;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class BrandsController : AppController
    {
        private readonly DataService _dataService;

        protected override async Task GetInputCombos()
        {
            ViewBag.ComboManufacturers = await _dataService.GetAsync<IEnumerable<SelectListItem>>(DataOperations.GetManufacturersCombo, null);
        }

        public BrandsController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetAsync<IEnumerable<IndexRowBrandViewModel>>(DataOperations.GetBrandsOrderByName, null));
        }


        // GET: Brands/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataService.GetAsync<DisplayBrandViewModel>(DataOperations.GetBrandDisplay, id);
            if (model == null) return NotFound(EntityNames.Brand);

            return View(model);
        }


        // GET: Brands/Create
        public async Task<IActionResult> Create(int manufacturerId)
        {
            var model = manufacturerId < 1 ? null : new CreateBrandViewModel { ManufacturerId = manufacturerId };

            return await ViewInput(model);
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBrandViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Brand);

            try
            {
                await _dataService.CreateAsync(DataOperations.CreateBrand, model);

                return Success($"Brand created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Brand.");
            return await ViewInput(model);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            var model = await _dataService.GetAsync<EditBrandViewModel>(DataOperations.GetBrandEditModel, id);
            if (model == null) return NotFound(EntityNames.Brand);

            return await ViewInput(model);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid(EntityNames.Brand);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Brand);

            try
            {
                await _dataService.UpdateAsync(DataOperations.UpdateBrand, model);

                return Success($"Brand updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Brand.");
            return await ViewInput(model);
        }


        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            var model = await _dataService.GetAsync<DisplayBrandViewModel>(DataOperations.GetBrandDisplay, id);
            if (model == null) return NotFound(EntityNames.Brand);

            return View(model);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            try
            {
                await _dataService.DeleteAsync(DataOperations.DeleteBrand, id);

                return Success("Brand deleted.");
            }

            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Brand.");
            return await Delete(id);
        }
    }
}
