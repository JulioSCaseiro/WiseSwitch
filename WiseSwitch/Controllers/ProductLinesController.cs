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


        // GET: ProductLines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductLine model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalid(model, nameof(Create));

            try
            {
                await _dataUnit.ProductLines.CreateAsync(
                    new ProductLine {
                        Name = model.Name,
                        Brand = model.Brand
                    });

                TempData["LayoutMessageSuccess"] = $"Product Line created: {model.Name}.";
                return RedirectToAction(nameof(Index));
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Line.");
            return View(model);
        }


        // GET: ProductLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(nameof(ProductLine));

            var productLine = await _dataUnit.ProductLines.GetAsNoTrackingByIdAsync(id.Value);
            if (productLine == null) return NotFound(nameof(ProductLine));

            return View(productLine);
        }

        // POST: ProductLines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductLine model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalid(model, nameof(Edit));

            try
            {
                _dataUnit.ProductLines.Update(model);
                await _dataUnit.SaveChangesAsync();

                TempData["LayoutMessageSuccess"] = $"Product Line updated: {model.Name}.";
                return RedirectToAction(nameof(Index));
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
            return View(model);
        }


        // GET: ProductLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(nameof(ProductLine));

            //var productLine = await _dataUnit.ProductLines.GetIfDeletableAsync(id.Value);
            //if (productLine == null) return NotFound(nameof(ProductLine));

            //var productSeries = await _dataUnit.Brands.GetProductSeriesNamesOfProductLineAsync(id.Value);

            //if (productSeries.Any())
            //{
            //    ViewBag.IsDeletable = false;
            //    ViewBag.BrandsNames = productSeries;
            //}
            //else
            //{
            //    ViewBag.IsDeletable = true;
            //}

            return View(/*productLine*/);
        }

        // POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.ProductLines.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
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

        private IActionResult ModelStateInvalid(ProductLine model, string viewName)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the ProductLine was not accepted. Review the input and try again.");

            return View(viewName, model);
        }

        #endregion private helper methods
    }
}
