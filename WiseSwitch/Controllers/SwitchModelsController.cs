using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services.Api;
using WiseSwitch.Services.Data;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.SwitchModel;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class SwitchModelsController : AppController
    {
        private readonly DataService _dataService;

        protected override async Task GetInputCombos()
        {
            var getComboBrands = await _dataService.GetAsync<IEnumerable<SelectListItem>>(ApiUrls.GetBrandsCombo, null);
            if (getComboBrands.IsSuccess)
            {
                ViewBag.ComboBrands = getComboBrands.Result;
            }
            else throw new Exception("Could not get input combo from API.");

            var getComboFirmwareVersions = await _dataService.GetAsync<IEnumerable<SelectListItem>>(ApiUrls.GetFirmwareVersionsCombo, null);
            if (getComboFirmwareVersions.IsSuccess)
            {
                ViewBag.ComboFirmwareVersions = getComboFirmwareVersions.Result;
            }
            else throw new Exception("Could not get input combo from API.");
        }

        public SwitchModelsController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: SwitchModels
        public async Task<IActionResult> Index()
        {
            var getAll = await _dataService.GetAsync<IEnumerable<IndexRowSwitchModelViewModel>>(ApiUrls.GetAllSwitchModels);

            return ManageGetDataResponse<IEnumerable<IndexRowSwitchModelViewModel>>(getAll);
        }


        // GET: SwitchModels/5
        public async Task<IActionResult> Details(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            // Try get model.
            var getModel = await _dataService.GetAsync<DisplaySwitchModelViewModel>(ApiUrls.GetSwitchModelDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplaySwitchModelViewModel>(getModel);
        }


        // GET: SwitchModels/Create
        public async Task<IActionResult> Create(int productSeriesId)
        {
            // Default behavior - no ID has been given for the SwitchModel's Product Series.
            if (productSeriesId < 1)
            {
                return await ViewInput(null);
            }

            // -- If the user gave an ID for the Switch Model's Product Series --

            // Get Dependency Chain IDs.
            var getDependencyChainIds = await _dataService.GetAsync<ProductSeriesDependencyChainIds>(ApiUrls.GetProductSeriesDependencyChainIds, productSeriesId);
            if (getDependencyChainIds.IsSuccess)
            {
                if (getDependencyChainIds.Result is ProductSeriesDependencyChainIds dependencyChainIds)
                {
                    if (dependencyChainIds == null) return NotFound(EntityNames.ProductSeries);

                    var model = new CreateSwitchModelViewModel
                    {
                        ProductSeriesId = productSeriesId,
                        ProductLineId = dependencyChainIds.ProductLineId,
                        BrandId = dependencyChainIds.BrandId,
                    };

                    return await ViewInput(model);
                }
            }

            return ResponseIsNotSuccessful(getDependencyChainIds);
        }

        // POST: SwitchModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSwitchModelViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.SwitchModel);

            // Try create SwitchModel.
            var create = await _dataService.CreateAsync(ApiUrls.CreateSwitchModel, model);

            // Resolve response.
            return ManageInputResponse(create);
        }

        // GET: SwitchModels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            // Try to get model.
            var getModel = await _dataService.GetAsync<EditSwitchModelViewModel>(ApiUrls.GetSwitchModelEditModel, id);

            // Resolve response.
            return ManageGetDataResponse<EditSwitchModelViewModel>(getModel);
        }

        // POST: SwitchModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSwitchModelViewModel model)
        {
            // Check given ID is valid.
            if (model.Id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.SwitchModel);

            // Try update SwitchModel.
            var update = await _dataService.UpdateAsync(ApiUrls.UpdateSwitchModel, model);

            // Resolve response.
            return ManageInputResponse(update);
        }


        // GET: SwitchModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            // Try get Model.
            var getModel = await _dataService.GetAsync<DisplaySwitchModelViewModel>(ApiUrls.GetSwitchModelDisplay, id);

            // Resolve response.
            return ManageGetDataResponse<DisplaySwitchModelViewModel>(getModel);
        }

        // POST: SwitchModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check given ID is valid.
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            // Try delete SwitchModel.
            var delete = await _dataService.DeleteAsync(ApiUrls.DeleteSwitchModel, id);

            // Resolve response.
            return ManageDeleteResponse(delete);
        }
    }
}
