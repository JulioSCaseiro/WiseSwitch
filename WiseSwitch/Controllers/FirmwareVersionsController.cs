﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WiseSwitch.Data;
using WiseSwitch.Data.Entities;
using WiseSwitch.ViewModels;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class FirmwareVersionsController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public FirmwareVersionsController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
        }


        // GET: FirmwareVersions
        public async Task<IActionResult> Index()
        {
            return View(await _dataUnit.FirmwareVersions.GetAllOrderByVersionAsync());
        }


        // GET: FirmwareVersions/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataUnit.FirmwareVersions.GetDisplayViewModelAsync(id);
            if (model == null) return NotFound("Firmware Version");

            return View(model);
        }


        // GET: FirmwareVersions/Create
        public async Task<IActionResult> Create()
        {
            return await ViewInputAsync(null);
        }

        // POST: FirmwareVersions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FirmwareVersion model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                await _dataUnit.FirmwareVersions.CreateAsync(new FirmwareVersion
                {
                    Version = model.Version,
                    LaunchDate = model.LaunchDate,
                });
                await _dataUnit.SaveChangesAsync();

                return Success($"Firmware Version created: {model.Version}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Firmware Version.");
            return await ViewInputAsync(model);
        }


        // GET: FirmwareVersions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound("Firmware Version");

            var model = await _dataUnit.FirmwareVersions.GetAsNoTrackingByIdAsync(id.Value);
            if (model == null) return NotFound("Firmware Version");

            return await ViewInputAsync(model);
        }

        // POST: FirmwareVersions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FirmwareVersion model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model);

            try
            {
                _dataUnit.FirmwareVersions.Update(model);
                await _dataUnit.SaveChangesAsync();

                return Success($"Firmware Version updated: {model.Version}.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _dataUnit.FirmwareVersions.ExistsAsync(model.Id))
                {
                    return NotFound("Firmware Version");
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Firmware Version.");
            return await ViewInputAsync(model);
        }


        // GET: FirmwareVersions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound("Firmware Version");

            var model = await _dataUnit.FirmwareVersions.GetDisplayViewModelAsync(id.Value);
            if (model == null) return NotFound("Firmware Version");

            return View(model);
        }

        // POST: FirmwareVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataUnit.FirmwareVersions.DeleteAsync(id);
                await _dataUnit.SaveChangesAsync();

                return Success("Firmware Version deleted.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException innerEx)
                {
                    if (innerEx.Message.Contains("FK_SwitchModels_FirmwareVersions_DefaultFirmwareVersionId"))
                    {
                        return RedirectToAction(nameof(Delete), id);
                    }
                }
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Firmware Version.");
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

        private async Task<IActionResult> ModelStateInvalid(FirmwareVersion model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Firmware Version was not accepted. Review the input and try again.");

            return await ViewInputAsync(model);
        }

        private async Task<IActionResult> ViewInputAsync(FirmwareVersion model)
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
