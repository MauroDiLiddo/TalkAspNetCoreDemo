using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Models.Enums;
using TalkAspNetCoreDemo.Models.ViewModels;

namespace TalkAspNetCoreDemo.CustomTagHelper
{
    public class PriceTagHelper : TagHelper
    {
        [HtmlAttributeName("for-title")]
        public string Title { get; set; }

        [HtmlAttributeName("is-show-icon")]
        public bool IsShowIcon { get; set; }

        [HtmlAttributeName("for-price")]
        public Price CoursePrice { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                if (CoursePrice == null)
                {
                    base.Process(context, output);
                    return;
                }

                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.PreContent.SetHtmlContent(@"<div class=""row""><div class=""col-sm-12"">");

                var _contentHtml = new StringBuilder(string.Empty);
                if (!String.IsNullOrEmpty(Title))
                    _contentHtml.AppendFormat("<h4 class=\"border-bottom border-dark mb-1\">{0}</h4>", Title);

                _contentHtml.Append(@"<div class=""text-right mt-1"">");

                if (CoursePrice.DiscountPrice.Equals(CoursePrice.FullPrice))
                {
                    _contentHtml.Append("<p>");

                    if (IsShowIcon)
                        _contentHtml.Append("<i class=\"fas fa-money-check\"></i>");

                    _contentHtml.Append("&nbsp;&nbsp;");
                    _contentHtml.AppendFormat(@"<span class=""card-text font-weight-bold"">{0}</span></p>", CoursePrice.FullPrice.ToString());
                }
                else
                {
                    _contentHtml.Append("<p>");

                    if (IsShowIcon)
                        _contentHtml.Append("<i class=\"fas fa-money-check\"></i>");

                    _contentHtml.Append("&nbsp;&nbsp;");
                    _contentHtml.AppendFormat(@"<span class=""card-text text-danger small""><s>{0}</s></span>", CoursePrice.FullPrice.ToString());
                    _contentHtml.AppendFormat(@"&nbsp;&nbsp;<span class=""card-text font-weight-bold"">{0}</span></p>", CoursePrice.DiscountPrice.ToString());
                }

                _contentHtml.Append("</div>");

                output.Content.SetHtmlContent(_contentHtml.ToString());
                output.PostContent.SetHtmlContent("</div></div>");
                output.Attributes.Clear();
            }
            catch (Exception ex) { }

            return;
        }
    }
}
