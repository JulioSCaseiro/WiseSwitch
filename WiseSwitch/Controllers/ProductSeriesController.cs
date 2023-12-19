using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services;
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
            ViewBag.ComboBrands = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboBrands, null);
        }

        public ProductSeriesController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: ProductSeries
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetDataAsync<IEnumerable<IndexRowProductSeriesViewModel>>(DataOperations.GetAllProductSeriesOrderByName, null));
        }


        // GET: ProductSeries/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataService.GetDataAsync<DisplayProductSeriesViewModel>(DataOperations.GetDisplayProductSeries, id);
            if (model == null) return NotFound(EntityNames.ProductSeries);

            return View(model);
        }


        // GET: ProductSeries/Create
        public async Task<IActionResult> Create(int productLineId)
        {
            CreateProductSeriesViewModel model;

            if (productLineId < 1)
            {
                model = null;
            }
            else
            {
                var brandId = await _dataService.GetDataAsync<int>(DataOperations.GetBrandIdOfProductLine, productLineId);
                if (brandId < 1) return NotFound(EntityNames.Brand);

                model = new CreateProductSeriesViewModel
                {
                    BrandId = brandId,
                    ProductLineId = productLineId,
                };
            }

            return await ViewInput(model);
        }

        // POST: ProductSeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductSeriesViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductSeries);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateProductSeries, model);

                return Success($"Product Series created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Series.");
            return await ViewInput(model);
        }


        // GET: ProductSeries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            var model = await _dataService.GetDataAsync<EditProductSeriesViewModel>(DataOperations.GetEditModelProductSeries, id);
            if (model == null) return NotFound(EntityNames.ProductSeries);

            return await ViewInput(model);
        }

        // POST: ProductSeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductSeriesViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductSeries);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateProductSeries, model);

                return Success($"Product Series updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Product Series.");
            return await ViewInput(model);
        }


        // GET: ProductSeries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            var model = await _dataService.GetDataAsync<DisplayProductSeriesViewModel>(DataOperations.GetDisplayProductSeries, id);
            if (model == null) return NotFound(EntityNames.ProductSeries);

            return View(model);
        }

        // POST: ProductSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductSeries);

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteProductSeries, id);

                return Success("Product Series deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Product Series.");
            return await Delete(id);
        }
    }
}
