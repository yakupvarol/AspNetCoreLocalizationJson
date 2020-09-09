using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{
    public class NewsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id:int}")]
        public IActionResult Index(int? id)
        {
            return View();
        }
    }
}
