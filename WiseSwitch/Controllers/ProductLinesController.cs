using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services;
using WiseSwitch.Utils;
using WiseSwitch.ViewModels.Entities.ProductLine;

namespace WiseSwitch.Controllers
{
    [Authorize(Roles = "Admin,Technician")]
    public class ProductLinesController : AppController
    {
        private readonly DataService _dataService;

        protected override async Task GetInputCombos()
        {
            ViewBag.ComboBrands = await _dataService.GetAsync<IEnumerable<SelectListItem>>(DataOperations.GetBrandsCombo, null);
        }

        public ProductLinesController(DataService dataService)
        {
            _dataService = dataService;
        }


        // GET: ProductLines
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetAsync<IEnumerable<IndexRowProductLineViewModel>>(DataOperations.GetProductLinesOrderByName, null));
        }


        // GET: ProductLines/5
        public async Task<IActionResult> Details(int id)
        {
            var model = await _dataService.GetAsync<DisplayProductLineViewModel>(DataOperations.GetProductLineDisplay, id);
            if (model == null) return NotFound(EntityNames.ProductLine);

            return View(model);
        }


        // GET: ProductLines/Create
        public async Task<IActionResult> Create(int brandId)
        {
            var model = brandId < 1 ? null : new CreateProductLineViewModel { BrandId = brandId };

            return await ViewInput(model);
        }

        // POST: ProductLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductLineViewModel model)
        {
            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductLine);

            try
            {
                await _dataService.CreateAsync(DataOperations.CreateProductLine, model);

                return Success($"Product Line created: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not create Product Line.");
            return await ViewInput(model);
        }


        // GET: ProductLines/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            var model = await _dataService.GetAsync<EditProductLineViewModel>(DataOperations.GetProductLineEditModel, id);
            if (model == null) return NotFound(EntityNames.ProductLine);

            return await ViewInput(model);
        }

        // POST: ProductLines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductLineViewModel model)
        {
            if (model.Id < 1) return IdIsNotValid(EntityNames.ProductLine);

            if (!ModelState.IsValid)
                return await ModelStateInvalid(model, EntityNames.ProductLine);

            try
            {
                await _dataService.UpdateAsync(DataOperations.UpdateProductLine, model);

                return Success($"Product Line updated: {model.Name}.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not update Product Line.");
            return await ViewInput(model);
        }


        // GET: ProductLines/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            var productLine = await _dataService.GetAsync<DisplayProductLineViewModel>(DataOperations.GetProductLineDisplay, id);
            if (productLine == null) return NotFound(EntityNames.ProductLine);

            return View(productLine);
        }

        // POST: ProductLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id < 1) return IdIsNotValid(EntityNames.ProductLine);

            try
            {
                await _dataService.DeleteAsync(DataOperations.DeleteProductLine, id);

                return Success("Product Line deleted.");
            }
            catch { }

            ModelState.AddModelError(string.Empty, "Could not delete Product Line.");
            return await Delete(id);
        }
    }
}
