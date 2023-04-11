using INTEX2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX2.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-links")]
    public class PaginationTagHelper : TagHelper
    {
        // Dynamically create page links
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageLinks { get; set; }
        public string PageAction { get; set; }
        // Added for styling
        public string PageClass { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            // Limit the number of page links displayed
            int maxPageLinks = 5;
            int startPageLink = Math.Max(1, PageLinks.CurrentPage - (int)Math.Floor((decimal)maxPageLinks / 2));
            int endPageLink = Math.Min(PageLinks.TotalPages, startPageLink + maxPageLinks - 1);

            // Add ellipsis at the beginning if necessary
            if (startPageLink > 1)
            {
                TagBuilder ellipsis = new TagBuilder("span");
                ellipsis.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(ellipsis);
            }
            
            for (int i = startPageLink; i <= endPageLink; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

                // Added for button styling
                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageLinks.CurrentPage ? PageClassSelected : PageClassNormal);
                }

                tb.InnerHtml.Append(i.ToString());
                final.InnerHtml.AppendHtml(tb);
            }

            // Add ellipsis at the end if necessary
            if (endPageLink < PageLinks.TotalPages)
            {
                TagBuilder ellipsis = new TagBuilder("span");
                ellipsis.InnerHtml.Append("...");
                final.InnerHtml.AppendHtml(ellipsis);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}