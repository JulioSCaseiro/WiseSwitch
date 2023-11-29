using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.SwitchModel;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class SwitchModelsController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public SwitchModelsController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: SwitchModels
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.SwitchModels.GetAllOrderByModelNameAsync());
        }


        // GET: SwitchModels/Create
        public async Task<IActionResult> Create(int productSeriesId)
        {
            if (productSeriesId > 0)
            {
                int productLineId = await _dataUnit.ProductSeries.GetProductLineIdAsync(productSeriesId);
                int brandId = await _dataUnit.ProductLines.GetBrandIdAsync(productLineId);

                var model = new InputSwitchModelViewModel
                {
                    ProductSeriesId = productSeriesId,
                    ProductLineId = productLineId,
                    BrandId = brandId,
                };

                return await ViewInputAsync(model);
            }

            return await ViewInputAsync(null);
        }

        // POST: SwitchModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InputSwitchModelViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                await _dataUnit.SwitchModels.CreateAsync(new SwitchModel
                {
                    ModelName = model.ModelName,
                    ModelYear = model.ModelYear,
                    DefaultFirmwareVersionId = model.DefaultFirmwareVersionId,
                    ProductSeriesId = model.ProductSeriesId,
                });
                await _dataUnit.SaveChangesAsync();

                return Success($"Switch Model created: {model.ModelName}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Switch Model.");
            return await ViewInputAsync(model);
        }


        // GET: SwitchModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound("Switch Model");

            var model = await _dataUnit.SwitchModels.GetInputViewModelAsync(id.Value);
            if (model == null) return NotFound("Switch Model");

            return await ViewInputAsync(model);
        }

        // POST: SwitchModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InputSwitchModelViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                var switchModel = await _dataUnit.SwitchModels.GetForUpdateAsync(model.Id);

                switchModel.ModelName = model.ModelName;
                switchModel.ModelYear = model.ModelYear;
                switchModel.DefaultFirmwareVersionId = model.DefaultFirmwareVersionId;
                switchModel.ProductSeriesId = model.ProductSeriesId;

                await _dataUnit.SaveChangesAsync();

                return Success($"Switch Model updated: {model.ModelName}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.SwitchModels.ExistsAsync(model.Id))
                {
                    return NotFound("Switch Model");
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Switch Model.");
            return await ViewInputAsync(model);
        }


        // GET: SwitchModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound("Switch Model");

            var brand = await _dataUnit.SwitchModels.GetAsNoTrackingByIdAsync(id.Value);
            if (brand == null) return NotFound(nameof(brand));

            return View(brand);
        }

        // POST: SwitchModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.SwitchModels.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Switch Model deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Switch Model.");
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

        private async Task<IActionResult> ModelStateInvalid(InputSwitchModelViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Switch Model was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }

        private async Task<IActionResult> ViewInputAsync(InputSwitchModelViewModel model)
        {
            ViewBag.ComboBrands = await _dataUnit.Brands.GetComboBrandsAsync();
            ViewBag.ComboFirmwareVersions = await _dataUnit.FirmwareVersions.GetComboFirmwareVersionsAsync();

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
