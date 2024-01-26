using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;
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
            var getCombo = await _dataService.GetAsync<IEnumerable<SelectListItem>>(ApiUrls.GetManufacturersCombo, null);
            if (getCombo.IsSuccess)
            {
                ViewBag.ComboManufacturers = getCombo.Result;
            }
            else throw new Exception("Could not get input combo from API.");
        }

        public BrandsController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: Brands
        public async Task<IActionResult> Index()
        {
            //return await _helper.GetIndexAsync<IEnumerable<IndexRowBrandViewModel>>(EntityNames.Brand);
            var getAll = await _dataService.GetAsync<IEnumerable<IndexRowBrandViewModel>>(ApiUrls.GetAllBrands);
            
            return ManageGetDataResponse<IEnumerable<IndexRowBrandViewModel>>(getAll);
        }


        // GET: Brands/5
        public async Task<IActionResult> Details(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            // Try get model.
            var getModel = await _dataService.GetAsync<DisplayBrandViewModel>(ApiUrls.GetBrandDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayBrandViewModel>(getModel);
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

            // Try create Brand.
            var create = await _dataService.CreateAsync(ApiUrls.CreateBrand, model);

            // Resolve response.
            return ManageInputResponse(create);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            // Try to get model.
            var getModel = await _dataService.GetAsync<EditBrandViewModel>(ApiUrls.GetBrandEditModel, id);

            // Resolve response.
            return ManageGetDataResponse<EditBrandViewModel>(getModel);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBrandViewModel model)
        {
            // Check given ID is valid.
            if (model.Id < 1) return IdIsNotValid(EntityNames.Brand);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.Brand);

            // Try update Brand.
            var update = await _dataService.UpdateAsync(ApiUrls.UpdateBrand, model);

            // Resolve response.
            return ManageInputResponse(update);
        }


        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            // Try get Model.
            var getModel = await _dataService.GetAsync<DisplayBrandViewModel>(ApiUrls.GetBrandDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayBrandViewModel>(getModel);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.Brand);

            // Try delete Brand.
            var delete = await _dataService.DeleteAsync(ApiUrls.DeleteBrand, id);

            // Resolve response.
            return ManageDeleteResponse(delete);
        }
    }
}
