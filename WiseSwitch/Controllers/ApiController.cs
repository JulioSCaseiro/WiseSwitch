using Microsoft.AspNetCore.Mvc;
using WiseSwitch.Data;

namespace WiseSwitch.Controllers
{
    public class ApiController : Controller
    {
        private readonly IDataUnit _dataUnit;

        public ApiController(IDataUnit dataUnit)
        {
            _dataUnit = dataUnit;
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
            var listByBrand = await _dataUnit.ProductLines.GetComboProductLinesOfBrandAsync(brandId);
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
            var listByProductLine = await _dataUnit.ProductSeries.GetComboProductSeriesOfProductLineAsync(productLineId);
            return Ok(new { Successful = true, Combo = listByProductLine });
        }
    }
}
