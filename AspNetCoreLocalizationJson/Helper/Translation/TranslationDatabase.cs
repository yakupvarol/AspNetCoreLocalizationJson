using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLocalizationJson.Helper.Translation
{
    public class TranslationDatabase
    {
        private static Dictionary<string, Dictionary<string, string>> Translations = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "en", new Dictionary<string, string>
                {
                    //en/news
                    //en/fair

                    { "news", "news" }, //controller
                    { "fair", "blog" }, //controller
                    { "index", "index" } //common
                }

            },
            {
                "tr", new Dictionary<string, string>
                {
                    //News
                    //tr/haberler
                    //tr/haberler/liste
                    //tr/haberler/liste/1
                    
                    { "haberler", "news" }, //controller

                    { "liste", "index" }, //action - common
                    { "{id:int}", "id" }, //parametter - common

                    //Blog
                    //tr/icerikler
                    //tr/icerikler/liste
                    //tr/icerikler/liste/1
                    
                    { "icerikler", "blog" }, //controller
                    
                    { "index", "index" } //action - common

                    // controller dışındaki alanlar ortak kullanılabilir.
                }
            },
            {
                "fr", new Dictionary<string, string>
                {
                    //fr/nouvelles
                    //fr/nouvelles/maison
                    { "nouvelles", "news" }, //controller
                    { "maison", "index" }  //action 
                }
            },



        };

        public async Task<string> Resolve(string lang, string value)
        {
            var normalizedLang = lang.ToLowerInvariant();
            var normalizedValue = value.ToLowerInvariant();
            if (Translations.ContainsKey(normalizedLang) && Translations[normalizedLang].ContainsKey(normalizedValue))
            {
                return Translations[normalizedLang][normalizedValue];
            }
            return null;
        }
    }
}
