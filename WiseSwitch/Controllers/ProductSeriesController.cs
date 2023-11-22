using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels;

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


        // GET: ProductSeries/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInputAsync(null);
        }

        // POST: ProductSeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSeries model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                await _dataUnit.ProductSeries.CreateAsync(
                    new ProductSeries
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(nameof(ProductSeries));

            var productSeries = await _dataUnit.ProductSeries.GetAsNoTrackingByIdAsync(id.Value);
            if (productSeries == null) return NotFound(nameof(ProductSeries));

            return await ViewInputAsync(productSeries);
        }

        // POST: ProductSeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductSeries model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                _dataUnit.ProductSeries.Update(model);
                await _dataUnit.SaveChangesAsync();

                return Success($"Product Series updated: {model.Name}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.ProductSeries.ExistsAsync(model.Id))
                {
                    return NotFound(nameof(ProductSeries));
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update the current product series.");
            return await ViewInputAsync(model);
        }


        // GET: ProductSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(nameof(ProductSeries));

            var productLine = await _dataUnit.ProductSeries.GetAsNoTrackingByIdAsync(id.Value);
            if (productLine == null) return NotFound(nameof(ProductSeries));

            var switchModelsNames = await _dataUnit.SwitchModels.GetSwitchModelsNamesOfProductSeriesAsync(id.Value);

            if (switchModelsNames.Any())
            {
                ViewBag.IsDeletable = false;
                ViewBag.SwitchModelsNames = switchModelsNames;
            }
            else
            {
                ViewBag.IsDeletable = true;
            }

            return View(productLine);
        }

        // POST: ProductSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
                        ViewBag.ErrorTitle = "Can't delete this switch model.";
                        ViewBag.ErrorMessage =
                            "You can't delete this switch model" +
                            " because it has at least one product series registered in the database using the current switch model.";
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete the current product series.");
            return View(id);
        }

        #region private helper methods

        private IActionResult NotFound(string entityName)
        {
            var model = new NotFoundViewModel
            {
                Title = $"{entityName} not found",
                Message = $"The {entityName} you're looking for was not found."
            };

            return View(nameof(NotFound), model);
        }

        private async Task<IActionResult> ModelStateInvalid(ProductSeries model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Line was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }

        private async Task<IActionResult> ViewInputAsync(ProductSeries? model)
        {
            ViewBag.ComboProductLines = await _dataUnit.ProductLines.GetComboProductLinesAsync();
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
