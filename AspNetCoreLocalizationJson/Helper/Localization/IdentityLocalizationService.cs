using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace AspNetCoreLocalizationJson.Helper.Localization
{
    public class IdentityLocalizationService
    {
        private readonly IStringLocalizer _localizer;
        
        private readonly IHttpContextAccessor httpContextAccessor;
        public IdentityLocalizationService(IStringLocalizerFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var type = typeof(IdentityResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("IdentityResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            LangCulture();
            return _localizer[key];
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            LangCulture();
            return _localizer[key, parameter];
        }

        public void LangCulture()
        {   try
            {
                string currentCulture = SecurityHelp.XSS(httpContextAccessor.HttpContext.GetRouteValue("lang") as string);
                if (!string.IsNullOrEmpty(currentCulture))
                { CultureInfo.CurrentCulture = new CultureInfo(currentCulture); }
                else
                { DefaultCulture(); }
            }
            catch { DefaultCulture(); }
        }

        public void DefaultCulture()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en");
        }

    }

    public class IdentityResource
    {

    }
}
