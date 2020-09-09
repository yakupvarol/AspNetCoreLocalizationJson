using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Article()
        {
            return View();
        }
    }
}
