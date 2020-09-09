using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using System.Collections.Generic;
using System.Globalization;

namespace AspNetCoreLocalizationJson.Helper.Localization.Language
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            var options = new RequestLocalizationOptions();
            ConfigureOptions(options);
            app.UseRequestLocalization(options);
        }

        public static void ConfigureOptions(RequestLocalizationOptions options)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("tr"),
                new CultureInfo("en"),
                new CultureInfo("fr"),
                new CultureInfo("ru"),
            };

            options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = new[] {
                new RouteDataRequestCultureProvider()
                {
                    Options = options,
                    RouteDataStringKey = "lang",
                    UIRouteDataStringKey = "lang"
                }
            };
        }

    }
}

