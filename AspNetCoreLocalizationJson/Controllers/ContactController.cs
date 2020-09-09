using AspNetCoreLocalizationJson.Helper.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{   
    public class ContactController : BaseController
    {
        private readonly IdentityLocalizationService _localizer;
        public ContactController(IdentityLocalizationService localizer)
        {
            _localizer = localizer;
        }

        [Route("{lang:lang}/communication/{id:int}")]
        [Route("{lang:lang}/iletisim/{id:int}", Name = RouteNames.iletisim)]
        [Route("{lang:lang}/contact/{id:int}", Name = RouteNames.contact)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
