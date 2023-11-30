using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.ProductSeries;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ProductSeriesController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public ProductSeriesController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: ProductSeries
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.ProductSeries.GetAllOrderByName());
        }


        // GET: ProductSeries/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 1) return IdIsNotValid("Product Series");

            var model = await _dataUnit.ProductSeries.GetDisplayViewModelAsync(id);
            if (model == null) return NotFound("Product Series");

            return View(model);
        }


        // GET: ProductSeries/Create
        public async Task<IActionResult> Create(int productLineId)
        {
            if (productLineId > 0)
            {
                var brandId = await _dataUnit.ProductLines.GetBrandIdAsync(productLineId);

                var model = new InputProductSeriesViewModel
                {
                    BrandId = brandId,
                    ProductLineId = productLineId,
                };

                return await ViewInputAsync(model);
            }

            return await ViewInputAsync(null);
        }

        // POST: ProductSeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputProductSeriesViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                await _dataUnit.ProductSeries.CreateAsync(new ProductSeries
                {
                    Name = model.Name,
                    ProductLineId = model.ProductLineId
                });
                await _dataUnit.SaveChangesAsync();

                return Success($"Product Series created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Series.");
            return await ViewInputAsync(model);
        }


        // GET: ProductSeries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid("Product Series");

            var model = await _dataUnit.ProductSeries.GetInputViewModelAsync(id);
            if (model == null) return NotFound("Product Series");

            return await ViewInputAsync(model);
        }

        // POST: ProductSeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InputProductSeriesViewModel model)
        {
            if (model.Id  < 1) return IdIsNotValid("Product Series");

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                var productSeries = await _dataUnit.ProductSeries.GetForUpdateAsync(model.Id);

                productSeries.Name = model.Name;
                productSeries.ProductLineId = model.ProductLineId;

                await _dataUnit.SaveChangesAsync();

                return Success($"Product Series updated: {model.Name}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.ProductSeries.ExistsAsync(model.Id))
                {
                    return NotFound("Product Series");
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Product Series.");
            return await ViewInputAsync(model);
        }


        // GET: ProductSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id < 1) return IdIsNotValid("Product Series");

            var model = await _dataUnit.ProductSeries.GetDisplayViewModelAsync(id.Value);
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
                await _dataUnit.ProductSeries.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Product Series deleted.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_SwitchModels_ProductSeries_ProductSeriesId"))
                    {
                        return RedirectToAction(nameof(Delete), id);
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Product Series.");
            return View(id);
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

        private async Task<IActionResult> ModelStateInvalid(InputProductSeriesViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Series was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }


        private async Task<IActionResult> ViewInputAsync(InputProductSeriesViewModel model)
        {
            ViewBag.ComboBrands = await _dataUnit.Brands.GetComboBrandsAsync();
            return View(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        #endregion private helper methods
    }
}
