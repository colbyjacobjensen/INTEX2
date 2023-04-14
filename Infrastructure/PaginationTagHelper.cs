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

            for (int i = 1; i <= PageLinks.TotalPages; i++)
            {
                // Limit the number of page links displayed
                if (Math.Abs(i - PageLinks.CurrentPage) > 2)
                {
                    continue;
                }

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

            tho.Content.AppendHtml(final);
        }
    }
}
