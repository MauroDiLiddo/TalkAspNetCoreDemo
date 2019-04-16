using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkAspNetCoreDemo.CustomTagHelper
{
    [HtmlTargetElement("rate")]
    public class RatingTagHelper : TagHelper
    {
        [HtmlAttributeName("for-title")]
        public string Title { get; set; }

        [HtmlAttributeName("for-rating-value")]
        public double RatingValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.PreContent.SetHtmlContent(@"<div class=""row""><div class=""col-sm-12"">");
            var _contentHtml = new StringBuilder(string.Empty);
            if (!String.IsNullOrEmpty(Title))
                _contentHtml.AppendFormat("<h4 class=\"border-bottom border-dark mb-1\">{0}</h4>", Title);

            _contentHtml.AppendFormat(@"<div class=""text-right mt-1 rating"" title=""{0}"">", RatingValue);

            for (int i = 1; i <= RatingValue; i++)
            {
                if (RatingValue >= i)
                {
                    _contentHtml.Append(@"<i class=""fas fa-star""></i>");
                    continue;
                }

                if (RatingValue > i - 1)
                {
                    _contentHtml.Append(@"<i class=""fas fa-star-half-alt""></i>");
                    continue;
                }

                _contentHtml.Append(@"<i class=""far fa-star""></i>");
            }

            _contentHtml.Append("</div>");

            output.Content.SetHtmlContent(_contentHtml.ToString());
            output.PostContent.SetHtmlContent("</div></div>");
            output.Attributes.Clear();
        }
    }
}
