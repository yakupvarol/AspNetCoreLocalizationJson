using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreLocalizationJson.Models;
using AspNetCoreLocalizationJson.Helper.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace AspNetCoreLocalizationJson.Controllers
{
    public class HomeController : BaseController
    {
        //private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IdentityLocalizationService _localizer;

        //public HomeController(IStringLocalizer<HomeController> localizer)
        public HomeController(IdentityLocalizationService localizer)
        {
            _localizer = localizer;
        }
       
        public IActionResult Index()
        {
            /*
            string currentCulture = "fr";
            if (!string.IsNullOrEmpty(currentCulture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(currentCulture);
            }

            /*
            ViewData["Hello"] = _localizer["Hello"]; //IStringLocalizer<HomeController>
            ViewData["Hello"] = _localizer.GetLocalizedHtmlString("Hello");
            */
           
            ViewBag.Hello = _localizer.GetLocalizedHtmlString("Hello");

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
    }
}
