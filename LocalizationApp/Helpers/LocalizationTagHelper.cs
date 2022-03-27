using LocalizationApp.Resources;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace LocalizationApp.Helpers
{
    [HtmlTargetElement("lang")]
    public class LocalizationTagHelper : TagHelper
    {
        private readonly ILocalizationService localizationService;

        public LocalizationTagHelper(ILocalizationService localizationService)
        {
            this.localizationService = localizationService;
        }

        [HtmlAttributeName("key")]
        public string LocalizationKey { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "LocalizationTag";
            output.TagMode = TagMode.StartTagAndEndTag;
            var data = localizationService.GetByKey(LocalizationKey);
            output.PreContent.SetHtmlContent(data);
        }
    }
}
