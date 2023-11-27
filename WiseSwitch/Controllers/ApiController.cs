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


        public async Task<IActionResult> ComboProductLines(int? brandId)
        {
            if (brandId == null || brandId.Value < 1)
            {
                // List all.
                var listAll = await _dataUnit.ProductLines.GetComboProductLinesAsync();
                return Ok(new { Successful = true, Combo = listAll });
            }

            // List by Brand.
            var listByBrand = await _dataUnit.ProductLines.GetComboProductLinesOfBrandAsync(brandId.Value);
            return Ok(new { Successful = true, Combo = listByBrand });
        }
    }
}
