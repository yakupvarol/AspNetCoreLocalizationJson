using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreLocalizationJson.Helper.Filter
{
    public class GeneralFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = HttpHelper.HttpContext.Request.Path.Value; //HttpHelper Örnek

            var lng = SecurityHelp.XSS(context.RouteData.Values["lang"] as string);

            //
            context.HttpContext.Response.Cookies.Append(enumCookie.AspNetCoreLang.ToString(), lng);

            //
            //context.HttpContext.Response.Headers.Remove(MyStaticNames.Location);
            context.HttpContext.Response.Headers.Add(MyStaticNames.Location, lng);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
