using AspNetCoreLocalizationJson.Helper.Localization.Language;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class AboutUsController : BaseController
    {
        
        public IActionResult Index()
        {

            return View();
        }
    }
}
