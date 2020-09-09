using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreLocalizationJson.Controllers
{
    public abstract class BaseController : Controller
    {
        private string _currentLanguage;

        public IActionResult RedirectToDefaultLanguage()
        {
            return RedirectToAction("Index", new { lang = CurrentLanguage });
        }

        private string CurrentLanguage
        {
            get
            {
                if (string.IsNullOrEmpty(_currentLanguage))
                {
                    var feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }
                return _currentLanguage;
            }
        }

    }
}
