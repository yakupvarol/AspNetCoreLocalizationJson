using AspNetCoreLocalizationJson.Helper.Localization;
public static class MyStaticLocalizer
{
    private static IdentityLocalizationService _localizer;

    public static void Configure(IdentityLocalizationService httpContextAccessor)
    {
        _localizer = httpContextAccessor;
    }

    public static string localizer(string key)
    {
        return _localizer.GetLocalizedHtmlString(key);
    }
}
