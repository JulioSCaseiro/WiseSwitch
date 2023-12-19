using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Dtos;
using WiseSwitch.Services;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.SwitchModel;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class SwitchModelsController : Controller
    {
        private readonly DataService _dataService;

        public SwitchModelsController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: SwitchModels
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetDataAsync<IEnumerable<IndexRowSwitchModelViewModel>>(DataOperations.GetAllSwitchModelsOrderByModelName, null));
        }


        // GET: SwitchModels/Create
        public async Task<IActionResult> Create(int productSeriesId)
        {
            CreateSwitchModelViewModel model;

            if (productSeriesId < 1)
            {
                model = null;
            }
            else
            {
                // Get Dependency Chain IDs.
                var dependencyChainIds = await _dataService.GetDataAsync<ProductSeriesDependencyChainIds>(DataOperations.GetDependencyChainIdsOfProductSeries, productSeriesId);
                if (dependencyChainIds == null) return NotFound("Product Series");

                // Create ViewModel.
                model = new CreateSwitchModelViewModel
                {
                    ProductSeriesId = productSeriesId,
                    ProductLineId = dependencyChainIds.ProductLineId,
                    BrandId = dependencyChainIds.BrandId,
                };
            }

            return await ViewCreate(model);
        }

        // POST: SwitchModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSwitchModelViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalidOnCreate(model);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateSwitchModel, model);

                return Success($"Switch Model created: {model.ModelName}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Switch Model.");
            return View(model);
        }


        // GET: SwitchModels/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid("Switch Model");

            var model = await _dataService.GetDataAsync<EditSwitchModelViewModel>(DataOperations.GetEditModelSwitchModel, id);
            if (model == null) return NotFound("Switch Model");

            return await ViewEdit(model);
        }

        // POST: SwitchModels/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSwitchModelViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid("Switch Model");

            if (!ModelState.IsValid)
                return await ModelStateInvalidOnEdit(model);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateSwitchModel, model);

                return Success($"Switch Model updated: {model.ModelName}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Switch Model.");
            return await ViewEdit(model);
        }


        // GET: SwitchModels/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid("Switch Model");

            var model = await _dataService.GetDataAsync<DisplaySwitchModelViewModel>(DataOperations.GetDisplaySwitchModel, id);
            if (model == null) return NotFound("Switch Model");

            return View(model);
        }

        // POST: SwitchModels/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid("Switch Model");

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteSwitchModel, id);

                return Success("Switch Model deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Switch Model.");
            return await Delete(id);
        }


        #region private helper methods

        private async Task GetInputCombosAsync()
        {
            ViewBag.ComboBrands = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboBrands, null);
            ViewBag.ComboFirmwareVersions = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboFirmwareVersions, null);
        }

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

        private async Task<IActionResult> ModelStateInvalidOnCreate(CreateSwitchModelViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Switch Model was not accepted. Review the input and try again.");

            return await ViewCreate(model);
        }

        private async Task<IActionResult> ModelStateInvalidOnEdit(EditSwitchModelViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Switch Model was not accepted. Review the input and try again.");

            return await ViewEdit(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ViewCreate(CreateSwitchModelViewModel model)
        {
            await GetInputCombosAsync();
            return View(nameof(Create), model);
        }

        private async Task<IActionResult> ViewEdit(EditSwitchModelViewModel model)
        {
            await GetInputCombosAsync();
            return View(nameof(Edit), model);
        }

        #endregion private helper methods
    }
}
