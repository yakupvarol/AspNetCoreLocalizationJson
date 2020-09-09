using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace AspNetCoreLocalizationJson.Helper.Localization.Language
{
    [HtmlTargetElement(Attributes = "is-active-language")]
    public class LanguageActiveTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-route-lang")]
        public string Language { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            if (ShouldBeActive())
            {
                MakeActive(output);
            }
            output.Attributes.RemoveAll("is-active-language");
        }

        private bool ShouldBeActive()
        {
            string currentLanguage = ViewContext.RouteData.Values["lang"].ToString();
            if (!string.IsNullOrWhiteSpace(Language) && Language.ToLower() != currentLanguage.ToLower())
            {
                return false;
            }
            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active") < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? "active"
                    : classAttr.Value.ToString() + " active");
            }
        }
    }
}
