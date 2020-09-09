using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;

public class MyStaticHelper
{
    private static IHttpContextAccessor httpContextAccessor;

    public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
    {
        httpContextAccessor = accessor;
    }

    public static string Lang
    {
        get 
        {
            return SecurityHelp.XSS(HttpHelper.HttpContext.GetRouteValue("lang") as string); //HttpHelper Örnek
            //return SecurityHelp.XSS(httpContextAccessor.HttpContext.GetRouteValue("lang") as string); 
        }
    }

    public static string CookieLang
    {
        get
        {
            string lng = null;
            var setCookie = httpContextAccessor.HttpContext.Response.Headers["Set-Cookie"];
            foreach (var item in setCookie)
            {
                if (item.IndexOf("AspNetCoreLang=") != -1)
                { lng = item.Replace("AspNetCoreLang=", "").Replace("; path=/", ""); }
            }

            if (lng == null)
            {
                lng=  httpContextAccessor.HttpContext.Request.Cookies[enumCookie.AspNetCoreLang.ToString()] as string; 
            }
            
            return lng;
        }
    }

    public static string HeaderLang
    {
        get { return httpContextAccessor.HttpContext.Response.Headers[MyStaticNames.Location]; }
    }

    public static string RouteRedirectLang(string lang)
    {
        return RouteReplaceLang(httpContextAccessor.HttpContext.Request.Path.Value, $"/{lang}");
        //return RouteReplaceLang(httpContextAccessor.HttpContext.Request.GetDisplayUrl(), $"/{lang}");
    }

    public static string RouteReplaceLang(string route, string lang)
    {
        string result = route;
        result = result.Replace("/tr", lang);
        result = result.Replace("/en", lang);
        result = result.Replace("/fr", lang);
        result = result.Replace("/ru", lang);
        return result;
    }
}