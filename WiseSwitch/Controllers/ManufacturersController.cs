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
    public class ManufacturersController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public ManufacturersController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.Manufacturers.GetAllOrderByName());
        }


        // GET: Manufacturers/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataUnit.Manufacturers.GetDisplayViewModelAsync(id);
            if (model == null) return NotFound(nameof(Manufacturer));

            return View(model);
        }


        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalid(model);

            try
            {
                await _dataUnit.Manufacturers.CreateAsync(new Manufacturer { Name = model.Name });
                await _dataUnit.SaveChangesAsync();

                return Success($"Manufacturer created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Manufacturer.");
            return View(model);
        }


        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound(nameof(Manufacturer));

            var manufacturer = await _dataUnit.Manufacturers.GetAsNoTrackingByIdAsync(id.Value);
            if (manufacturer == null) return NotFound(nameof(Manufacturer));

            return View(manufacturer);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manufacturer model)
        {
            if (!ModelState.IsValid)
                return ModelStateInvalid(model);

            try
            {
                _dataUnit.Manufacturers.Update(model);
                await _dataUnit.SaveChangesAsync();

                return Success($"Manufacturer updated: {model.Name}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.Manufacturers.ExistsAsync(model.Id))
                {
                    return NotFound(nameof(Manufacturer));
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Manufacturer.");
            return View(model);
        }


        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound(nameof(Manufacturer));

            var manufacturer = await _dataUnit.Manufacturers.GetAsNoTrackingByIdAsync(id.Value);
            if (manufacturer == null) return NotFound(nameof(Manufacturer));

            var brandNames = await _dataUnit.Brands.GetBrandNamesOfManufacturerAsync(id.Value);

            if (brandNames.Any())
            {
                ViewBag.IsDeletable = false;
                ViewBag.BrandsNames = brandNames;
            }
            else
            {
                ViewBag.IsDeletable = true;
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.Manufacturers.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Manufacturer deleted.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_Brands_Manufacturers_ManufacturerId"))
                    {
                        ViewBag.ErrorTitle = "Can't delete this Manufacturer.";
                        ViewBag.ErrorMessage =
                            "You can't delete this Manufacturer" +
                            " because it has at least one Brand registered in the database.";
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Manufacturer.");
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

        private IActionResult ModelStateInvalid(Manufacturer model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Manufacturer was not accepted. Review the input and try again.");

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
