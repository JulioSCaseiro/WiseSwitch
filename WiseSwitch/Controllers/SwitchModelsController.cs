﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Data.Dtos;
using WiseSwitch.Services;
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
            ViewBag.ComboBrands = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboBrands, null);
            ViewBag.ComboFirmwareVersions = await _dataService.GetDataAsync<IEnumerable<SelectListItem>>(DataOperations.GetComboFirmwareVersions, null);
        }

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

            return await ViewInput(model);
        }

        // POST: SwitchModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSwitchModelViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.SwitchModel);

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
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            var model = await _dataService.GetDataAsync<EditSwitchModelViewModel>(DataOperations.GetEditModelSwitchModel, id);
            if (model == null) return NotFound(EntityNames.SwitchModel);

            return await ViewInput(model);
        }

        // POST: SwitchModels/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditSwitchModelViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.SwitchModel);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateSwitchModel, model);

                return Success($"Switch Model updated: {model.ModelName}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Switch Model.");
            return await ViewInput(model);
        }


        // GET: SwitchModels/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            var model = await _dataService.GetDataAsync<DisplaySwitchModelViewModel>(DataOperations.GetDisplaySwitchModel, id);
            if (model == null) return NotFound(EntityNames.SwitchModel);

            return View(model);
        }

        // POST: SwitchModels/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.SwitchModel);

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteSwitchModel, id);

                return Success("Switch Model deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Switch Model.");
            return await Delete(id);
        }
    }
}
