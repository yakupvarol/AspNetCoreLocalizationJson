using AspNetCoreLocalizationJson.Helper.Route;

public class RouteLink
{
    public static string Product(RouteLinkDTO.Product dt)
    {
        return $"/{dt.lang}/{RouteNames.product}/{dt.val}/{dt.id}/{dt.productid}";
    }

    public static string ProductCookieLang(RouteLinkDTO.Product dt)
    {
        return $"/{MyStaticHelper.CookieLang}/{RouteNames.product}/{dt.val}/{dt.id}/{dt.productid}";
    }

    public static string ProductRouteLang(RouteLinkDTO.Product dt)
    {
        return $"/{MyStaticHelper.Lang}/{RouteNames.product}/{dt.val}/{dt.id}/{dt.productid}";
    }

    public static string ProductHeadersLang(RouteLinkDTO.Product dt)
    {
        return $"/{MyStaticHelper.HeaderLang}/{RouteNames.product}/{dt.val}/{dt.id}/{dt.productid}";
    }
}