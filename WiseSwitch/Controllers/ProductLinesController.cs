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
    public class ProductLinesController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public ProductLinesController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }
        
        
        // GET: ProductLines
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.ProductLines.GetAllOrderByName());
        }


        // GET: ProductLines/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataUnit.ProductLines.GetDisplayViewModelAsync(id);
            if (model == null) return NotFound("Product Line");

            return View(model);
        }

        // GET: ProductLines/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInputAsync(null);
        }

        // POST: ProductLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductLine model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                await _dataUnit.ProductLines.CreateAsync(
                    new ProductLine {
                        Name = model.Name,
                        BrandId = model.BrandId
                    });
                
                await _dataUnit.SaveChangesAsync();

                return Success($"Product Line created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Line.");
            return await ViewInputAsync(model);
        }


        // GET: ProductLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(nameof(ProductLine));

            var productLine = await _dataUnit.ProductLines.GetAsNoTrackingByIdAsync(id.Value);
            if (productLine == null) return NotFound(nameof(ProductLine));

            return await ViewInputAsync(productLine);
        }

        // POST: ProductLines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductLine model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                _dataUnit.ProductLines.Update(model);
                await _dataUnit.SaveChangesAsync();

                return Success($"Product Line updated: {model.Name}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.ProductLines.ExistsAsync(model.Id))
                {
                    return NotFound(nameof(ProductLine));
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update the current product line.");
            return await ViewInputAsync(model);
        }


        // GET: ProductLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(nameof(ProductLine));

            var productLine = await _dataUnit.ProductLines.GetAsNoTrackingByIdAsync(id.Value);
            if (productLine == null) return NotFound(nameof(ProductLine));

            var productSeriesNames = await _dataUnit.ProductSeries.GetProductSeriesNamesOfProductLineAsync(id.Value);

            if (productSeriesNames.Any())
            {
                ViewBag.IsDeletable = false;
                ViewBag.ProductSeriesNames = productSeriesNames;
            }
            else
            {
                ViewBag.IsDeletable = true;
            }

            return View(productLine);
        }

        // POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.ProductLines.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Product Line deleted.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_ProductSeries_ProductLines_ProductLineId"))
                    {
                        ViewBag.ErrorTitle = "Can't delete this product line.";
                        ViewBag.ErrorMessage =
                            "You can't delete this product line" +
                            " because it has at least one product series registered in the database using the current product line.";
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete the current product line.");
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

        private async Task<IActionResult> ModelStateInvalid(ProductLine model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Line was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }

        private async Task<IActionResult> ViewInputAsync(ProductLine model)
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
