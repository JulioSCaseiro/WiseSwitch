using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.ProductLine;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ProductLinesController : AppController
    {
        private readonly DataService _dataService;

        protected override async Task GetInputCombos()
        {
            var getCombo = await _dataService.GetAsync<IEnumerable<SelectListItem>>(ApiUrls.GetBrandsCombo, null);
            if (getCombo.IsSuccess)
            {
                ViewBag.ComboBrands = getCombo.Result;
            }
            else throw new Exception("Could not get input combo from API.");
        }

        public ProductLinesController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: ProductLines
        public async Task<IActionResult> Index()
        {
            var getAll = await _dataService.GetAsync<IEnumerable<IndexRowProductLineViewModel>>(ApiUrls.GetAllProductLines);

            return ManageGetDataResponse<IEnumerable<IndexRowProductLineViewModel>>(getAll);
        }


        // GET: ProductLines/5
        public async Task<IActionResult> Details(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            // Try get model.
            var getModel = await _dataService.GetAsync<DisplayProductLineViewModel>(ApiUrls.GetProductLineDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayProductLineViewModel>(getModel);
        }


        // GET: ProductLines/Create
        public async Task<IActionResult> Create(int brandId)
        {
            var model = brandId < 1 ? null : new CreateProductLineViewModel { BrandId = brandId };

            return await ViewInput(model);
        }

        // POST: ProductLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductLineViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductLine);

            // Try create ProductLine.
            var create = await _dataService.CreateAsync(ApiUrls.CreateProductLine, model);

            // Resolve response.
            return ManageInputResponse(create);
        }

        // GET: ProductLines/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            // Try to get model.
            var getModel = await _dataService.GetAsync<EditProductLineViewModel>(ApiUrls.GetProductLineEditModel, id);

            // Resolve response.
            return ManageGetDataResponse<EditProductLineViewModel>(getModel);
        }

        // POST: ProductLines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductLineViewModel model)
        {
            // Check given ID is valid.
            if (model.Id < 1) return IdIsNotValid(EntityNames.ProductLine);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductLine);

            // Try update ProductLine.
            var update = await _dataService.UpdateAsync(ApiUrls.UpdateProductLine, model);

            // Resolve response.
            return ManageInputResponse(update);
        }


        // GET: ProductLines/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            // Try get Model.
            var getModel = await _dataService.GetAsync<DisplayProductLineViewModel>(ApiUrls.GetProductLineDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayProductLineViewModel>(getModel);
        }

        // POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            // Try delete ProductLine.
            var delete = await _dataService.DeleteAsync(ApiUrls.DeleteProductLine, id);

            // Resolve response.
            return ManageDeleteResponse(delete);
        }
    }
}
