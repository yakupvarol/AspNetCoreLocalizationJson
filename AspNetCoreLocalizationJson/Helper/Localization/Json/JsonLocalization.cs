using System.Collections.Generic;

namespace AspNetCoreLocalizationJson.Helper.Localization
{
    class JsonLocalization
    {
        public string Key { get; set; }
        public Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
    }
}
