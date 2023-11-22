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
    public class BrandsController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public BrandsController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.Brands.GetAllOrderByNameAsync());
        }


        // GET: Brands/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInputAsync(null);
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                await _dataUnit.Brands.CreateAsync(new Brand
                {
                    Name = model.Name,
                    ManufacturerId = model.ManufacturerId
                });
                await _dataUnit.SaveChangesAsync();

                return Success($"Brand created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Brand.");
            return await ViewInputAsync(model);
        }


        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(nameof(Brand));

            var brand = await _dataUnit.Brands.GetAsNoTrackingByIdAsync(id.Value);
            if (brand == null) return NotFound(nameof(brand));

            return await ViewInputAsync(brand);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                _dataUnit.Brands.Update(model);
                await _dataUnit.SaveChangesAsync();

                return Success($"Brand updated: {model.Name}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.Brands.ExistsAsync(model.Id))
                {
                    return NotFound(nameof(Brand));
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Brand.");
            return await ViewInputAsync(model);
        }


        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(nameof(Brand));

            var brand = await _dataUnit.Brands.GetAsNoTrackingByIdAsync(id.Value);
            if (brand == null) return NotFound(nameof(brand));

            var productLinesNames = await _dataUnit.ProductLines.GetProductLinesNamesOfBrandAsync(id.Value);

            if (productLinesNames.Any())
            {
                ViewBag.IsDeletable = false;
                ViewBag.ProductLinesNames = productLinesNames;
            }
            else
            {
                ViewBag.IsDeletable = true;
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.Brands.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Brand deleted.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_ProductLines_Brands_BrandId"))
                    {
                        ViewBag.ErrorTitle = "Can't delete this Brand.";
                        ViewBag.ErrorMessage =
                            "You can't delete this Brand" +
                            " because it has at least one Product Line registered in the database.";
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Brand.");
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

        private async Task<IActionResult> ModelStateInvalid(Brand model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Brand was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }

        private async Task<IActionResult> ViewInputAsync(Brand? model)
        {
            ViewBag.ComboManufacturers = await _dataUnit.Manufacturers.GetComboManufacturersAsync();
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
