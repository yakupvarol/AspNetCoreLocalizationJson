using AspNetCoreLocalizationJson.Helper.Filter;
using AspNetCoreLocalizationJson.Helper.Localization;
using AspNetCoreLocalizationJson.Helper.Localization.Language;
using AspNetCoreLocalizationJson.Helper.Translation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AspNetCoreLocalizationJson
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //https://www.webtrainingroom.com/aspnetcore/globalization-localization
            //https://docs.microsoft.com/tr-tr/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper?view=aspnetcore-3.1
            //https://www.c-sharpcorner.com/article/a-better-approach-to-access-httpcontext-outside-a-controller-in-net-core-2-1/
            //https://overcoder.net/q/29305/%D0%B4%D0%BE%D1%81%D1%82%D1%83%D0%BF-%D0%BA-%D1%82%D0%B5%D0%BA%D1%83%D1%89%D0%B5%D0%BC%D1%83-httpcontext-%D0%B2-aspnet-core
            //https://docs.microsoft.com/tr-tr/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1

            // General
            services.AddControllersWithViews(config => { config.Filters.Add(new GeneralFilterAttribute()); })
           .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder) //[MiddlewareFilter(typeof(LocalizationPipeline))]
           .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix); //[MiddlewareFilter(typeof(LocalizationPipeline))]

            // Core
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Language Route
            services.Configure<RouteOptions>(options => { options.ConstraintMap.Add("lang", typeof(LanguageRouteConstraint)); });

            // Localization
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
            services.AddSingleton<IdentityLocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // IHttpContextAccessor Static
            MyStaticHelper.SetHttpContextAccessor(services.BuildServiceProvider().GetService<IHttpContextAccessor>()); //HttpHelper kullanabilirsiniz

            // Language TranslationRoute
            services.AddSingleton<TranslationTransformer>();
            services.AddSingleton<TranslationDatabase>();

            // Route Lowercase
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            //
            HttpHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>()); //MyStaticHelper yerine bunu kullanabilirsiniz.
            MyStaticLocalizer.Configure(app.ApplicationServices.GetRequiredService<IdentityLocalizationService>()); //Localizer static.

            //
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            var DefaultBrowserLanguage = options.Value.DefaultRequestCulture.Culture.TwoLetterISOLanguageName;

            //
            app.UseEndpoints(endpoints =>
            {
                //Language TranslationRoute
                endpoints.MapDynamicControllerRoute<TranslationTransformer>("{lang:lang}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDynamicControllerRoute<TranslationTransformer>("{**slug}");

                // Test Route
                endpoints.MapControllerRoute(name: "blog",
                pattern: "{lang:lang}/blog/{*article}",
                defaults: new { controller = "Blog", action = "Article" });

                // General Route
                endpoints.MapControllerRoute(
                   name: "LocalizedDefault",
                   pattern: "{lang:lang}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{*catchall}",
                  defaults: new { controller = "Home", action = "RedirectToDefaultLanguage", lang = DefaultBrowserLanguage });
            });
        }
    }
}
