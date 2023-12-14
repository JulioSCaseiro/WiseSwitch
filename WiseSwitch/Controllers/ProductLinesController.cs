using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Services;
using WiseSwitch.ViewModels;
using WiseSwitch.ViewModels.Entities.ProductLine;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ProductLinesController : Controller
    {
        private readonly DataService _dataService;

        public ProductLinesController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: ProductLines
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetAllProductLinesOrderByName, null));
        }


        // GET: ProductLines/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _dataService.GetDataAsync(DataOperations.GetDisplayProductLine, id));
        }


        // GET: ProductLines/Create
        public async Task<IActionResult> Create(int brandId)
        {
            var model = brandId < 1 ? null : new CreateProductLineViewModel { BrandId = brandId };

            return await ViewCreate(model);
        }

        // POST: ProductLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductLineViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalidOnCreate(model);

            try
            {
                await _dataService.PostDataAsync(DataOperations.CreateProductLine, model);

                return Success($"Product Line created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Line.");
            return await ViewCreate(model);
        }


        // GET: ProductLines/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid("Product Line");

            var model = await _dataService.GetDataAsync(DataOperations.GetEditModelProductLine, id);
            if (model == null) return NotFound("Product Line");

            if (model is EditProductLineViewModel productLine)
            {
                return await ViewEdit(productLine);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: ProductLines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductLineViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid("Product Line");

            if (!ModelState.IsValid)
                return await ModelStateInvalidOnEdit(model);

            try
            {
                await _dataService.PutDataAsync(DataOperations.UpdateProductLine, model);

                return Success($"Product Line updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Product Line.");
            return await ViewEdit(model);
        }


        // GET: ProductLines/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid("Product Line");

            var productLine = await _dataService.GetDataAsync(DataOperations.GetDisplayProductLine, id);
            if (productLine == null) return NotFound("Product Line");

            return View(productLine);
        }

        // POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid("Product Line");

            try
            {
                await _dataService.DeleteDataAsync(DataOperations.DeleteProductLine, id);

                return Success("Product Line deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Product Line.");
            return await Delete(id);
        }

        #region private helper methods

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

        private async Task<IActionResult> ModelStateInvalidOnCreate(CreateProductLineViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Line was not accepted. Review the input and try again.");

            return await ViewCreate(model);
        }

        private async Task<IActionResult> ModelStateInvalidOnEdit(EditProductLineViewModel model)
        {
            ModelState.AddModelError(
                string.Empty,
                "The input for the Product Line was not accepted. Review the input and try again.");

            return await ViewEdit(model);
        }

        private IActionResult Success(string message)
        {
            TempData["LayoutMessageSuccess"] = message;
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ViewCreate(CreateProductLineViewModel model)
        {
            ViewBag.ComboBrands = await _dataService.GetDataAsync(DataOperations.GetComboBrands, null);
            return View(nameof(Create), model);
        }

        private async Task<IActionResult> ViewEdit(EditProductLineViewModel model)
        {
            ViewBag.ComboBrands = await _dataService.GetDataAsync(DataOperations.GetComboBrands, null);
            return View(nameof(Edit), model);
        }

        #endregion private helper methods
    }
}
