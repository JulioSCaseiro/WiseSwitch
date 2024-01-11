using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.ProductSeries;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ProductSeriesController : AppController
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

        public ProductSeriesController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: ProductSeries
        public async Task<IActionResult> Index()
        {
            var getAll = await _dataService.GetAsync<IEnumerable<IndexRowProductSeriesViewModel>>(ApiUrls.GetAllProductSeries);

            return ManageGetDataResponse<IEnumerable<IndexRowProductSeriesViewModel>>(getAll);
        }


        // GET: ProductSeries/5
        public async Task<IActionResult> Details(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            // Try get model.
            var getModel = await _dataService.GetAsync<DisplayProductSeriesViewModel>(ApiUrls.GetProductSeriesDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayProductSeriesViewModel>(getModel);
        }


        // GET: ProductSeries/Create
        public async Task<IActionResult> Create(int productLineId)
        {
            // Default behavior - no ID has been given for the Product Series' Product Line.
            if (productLineId < 1)
            {
                return await ViewInput(null);
            }

            // -- If the user gives an ID for the ProductSeries' Product Line --

            var getBrandId = await _dataService.GetAsync<int>(ApiUrls.GetProductLineBrandId, productLineId);
            if (getBrandId.IsSuccess)
            {
                if (getBrandId.Result is int brandId)
                {
                    if (brandId < 1) return NotFound(EntityNames.Brand);

                    var model = new CreateProductSeriesViewModel
                    {
                        BrandId = brandId,
                        ProductLineId = productLineId,
                    };

                    return await ViewInput(model);
                }
            }

            return ResponseIsNotSuccessful(getBrandId);
        }

        // POST: ProductSeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductSeriesViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductSeries);

            // Try create ProductSeries.
            var create = await _dataService.CreateAsync(ApiUrls.CreateProductSeries, model);

            // Resolve response.
            return ManageInputResponse(create);
        }

        // GET: ProductSeries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            // Try to get model.
            var getModel = await _dataService.GetAsync<EditProductSeriesViewModel>(ApiUrls.GetProductSeriesEditModel, id);

            // Resolve response.
            return ManageGetDataResponse<EditProductSeriesViewModel>(getModel);
        }

        // POST: ProductSeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductSeriesViewModel model)
        {
            // Check given ID is valid.
            if (model.Id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductSeries);

            // Try update ProductSeries.
            var update = await _dataService.UpdateAsync(ApiUrls.UpdateProductSeries, model);

            // Resolve response.
            return ManageInputResponse(update);
        }


        // GET: ProductSeries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            // Try get Model.
            var getModel = await _dataService.GetAsync<DisplayProductSeriesViewModel>(ApiUrls.GetProductSeriesDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplayProductSeriesViewModel>(getModel);
        }

        // POST: ProductSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            // Try delete ProductSeries.
            var delete = await _dataService.DeleteAsync(ApiUrls.DeleteProductSeries, id);

            // Resolve response.
            return ManageDeleteResponse(delete);
        }
    }
}
