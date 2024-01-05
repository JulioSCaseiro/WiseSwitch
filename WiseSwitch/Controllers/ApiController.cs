using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WiseSwitch.Services;
using WiseSwitch.Utils;

namespace WiseSwitch.Controllers
{
    public class ApiController : Controller
    {
        private readonly DataService _dataService;

        public ApiController(DataService dataService)
        {
            _dataService = dataService;
        }


        public async Task<IActionResult> ComboProductLinesOfBrand(int brandId)
        {
            if (brandId < 1)
            {
                return Json(new
                {
                    Successful = false,
                    Message = "Cannot get Product Lines of Brand because the given Brand ID is not valid."
                });
            }

            // List by Brand.
            var listByBrand = await _dataService.GetAsync<IEnumerable<SelectListItem>>(ApiUrls.GetProductLinesComboOfBrand, brandId);
            return Ok(new { Successful = true, Combo = listByBrand });
        }

        public async Task<IActionResult> ComboProductSeriesOfProductLine(int productLineId)
        {
            if (productLineId < 1)
            {
                return Json(new
                {
                    Successful = false,
                    Message = "Cannot get Product Series of Product Line because the given Product Line ID is not valid."
                });
            }

            // List by ProductLine.
            var listByProductLine = await _dataService.GetAsync<IEnumerable<SelectListItem>>(ApiUrls.GetProductSeriesComboOfProductLine, productLineId);
            return Ok(new { Successful = true, Combo = listByProductLine });
        }
    }
}
