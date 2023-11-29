using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WiseSwitch.ViewModels;

namespace WiseSwitch.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Error/404")]
        public new IActionResult NotFound()
        {
            var model = new NotFoundViewModel
            {
                Title = "Not Found",
                Message = "The resource you are looking for was not found.",
            };
            
            Response.StatusCode = StatusCodes.Status404NotFound;
            
            return View(model);
        }
    }
}