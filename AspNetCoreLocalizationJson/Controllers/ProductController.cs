using AspNetCoreLocalizationJson.Helper.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IdentityLocalizationService _localizer;
        public ProductController(IdentityLocalizationService localizer)
        {
            _localizer = localizer;
        }

        [Route("{lang:lang}/"+ RouteNames.product +"/{val?}/{id:int}/{productid}", Name = RouteNames.product)]
        public IActionResult Index()
        {
            var Test = MyStaticLocalizer.localizer("Product");

            ViewBag.Product = _localizer.GetLocalizedHtmlString("Product");

            return View();
        }
    }
}
