using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.ProductSeries;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ProductSeriesController : Controller
    {
        private readonly DataService _dataService;

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
            return View(await _dataService.GetDataAsync<DisplayProductSeriesViewModel>(DataOperations.GetDisplayProductSeries, id));
        }


        // GET: ProductSeries/Create
        public async Task<IActionResult> Create(int productLineId)
        {
            if (productLineId > 0)
            {
                var brandId = await _dataService.GetDataAsync<int>(DataOperations.GetBrandIdOfProductLine, productLineId);

                if (brandId > 0)
                {
                    var model = new CreateProductSeriesViewModel
                    {
                        BrandId = brandId,
                        ProductLineId = productLineId,
                    };

                    return await ViewCreate(model);
                }
                else
                {
                    return View("Error");
                }
            }

            return await ViewCreate(null);
        }

        // POST: ProductSeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductSeriesViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalidOnCreate(model);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateProductSeries, model);

                return Success($"Product Series created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Series.");
            return await ViewCreate(model);
        }


        // GET: ProductSeries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid("Product Series");

            var model = await _dataService.GetDataAsync<EditProductSeriesViewModel>(DataOperations.GetEditModelProductSeries, id);
            if (model == null) return NotFound("Product Series");

            return await ViewEdit(model);
        }

        // POST: ProductSeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductSeriesViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid("Product Series");

            if (!ModelState.IsValid)
                return await ModelStateInvalidOnEdit(model);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateProductSeries, model);

                return Success($"Product Series updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Product Series.");
            return await ViewEdit(model);
        }


        // GET: ProductSeries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid("Product Series");

            var model = await _dataService.GetDataAsync<DisplayProductSeriesViewModel>(DataOperations.GetDisplayProductSeries, id);
            if (model == null) return NotFound("Product Series");

            return View(model);
        }

        // POST: ProductSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid("Product Series");

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteProductSeries, id);

                return Success("Product Series deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Product Series.");
            return await Delete(id);
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

        private async Task<IActionResult> ModelStateInvalidOnCreate(CreateProductSeriesViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Series was not accepted. Review the input and try again.");

            return await ViewCreate(model);
        }

        private async Task<IActionResult> ModelStateInvalidOnEdit(EditProductSeriesViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Series was not accepted. Review the input and try again.");

            return await ViewEdit(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ViewCreate(CreateProductSeriesViewModel model)
        {
            ViewBag.ComboBrands = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboBrands, null);
            return View(nameof(Create), model);
        }

        private async Task<IActionResult> ViewEdit(EditProductSeriesViewModel model)
        {
            ViewBag.ComboBrands = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboBrands, null);
            return View(nameof(Edit), model);
        }

        #endregion private helper methods
    }
}
