using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductSeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSeries model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalid(model, nameof(Create));

            try
            {
                await _dataUnit.ProductSeries.CreateAsync(
                    new ProductSeries {
                        Name = model.Name,
                        ProductLine = model.ProductLine
                    });

                TempData["LayoutMessageSuccess"] = $"Product Series created: {model.Name}.";
                return RedirectToAction(nameof(Index));
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Series.");
            return View(model);
        }

        // GET: ProductSeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(nameof(ProductSeries));

            var productSeries = await _dataUnit.ProductSeries.GetAsNoTrackingByIdAsync(id.Value);
            if (productSeries == null) return NotFound(nameof(ProductSeries));

            return View(productSeries);
        }

        // POST: ProductSeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductSeries model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalid(model, nameof(Edit));

            try
            {
                _dataUnit.ProductSeries.Update(model);
                await _dataUnit.SaveChangesAsync();

                TempData["LayoutMessageSuccess"] = $"Product Series updated: {model.Name}.";
                return RedirectToAction(nameof(Index));
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
            return View(model);
        }

        // GET: ProductSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(nameof(ProductSeries));

            //var productSeries = await _dataUnit.ProductSeriess.GetIfDeletableAsync(id.Value);
            //if (productSeries == null) return NotFound(nameof(ProductSeries));

            //var productSeries = await _dataUnit.Brands.GetProductSeriesNamesOfProductSeriesAsync(id.Value);

            //if (productSeries.Any())
            //{
            //    ViewBag.IsDeletable = false;
            //    ViewBag.BrandsNames = productSeries;
            //}
            //else
            //{
            //    ViewBag.IsDeletable = true;
            //}

            return View(/*productSeries*/);
        }

        // POST: ProductSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.ProductSeries.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_SwitchModels_ProductSeries_ProductSeriesId"))
                    {
                        ViewBag.ErrorTitle = "Can't delete this product series.";
                        ViewBag.ErrorMessage =
                            "You can't delete this product series" +
                            " because it has at least one switch model registered in the database using the current product series.";
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

        private IActionResult ModelStateInvalid(ProductSeries model, string viewName)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the ProductSeries was not accepted. Review the input and try again.");

            return View(viewName, model);
        }

        #endregion private helper methods
    }
}
