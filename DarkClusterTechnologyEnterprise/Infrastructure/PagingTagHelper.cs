using DarkClusterTechnologyEnterprise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkClusterTechnologyEnterprise.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PagingTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PagingTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();

        public bool PageClassEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClasNormal { get; set; }
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (PageModel.CurrentPage == 1)
            {
                if (PageModel.TotalPages > 1)
                {
                    TagBuilder btns = AddNavBtnsNext();
                    TagBuilder result = FirstPage();
                    output.Content.AppendHtml(result.InnerHtml);
                    output.Content.AppendHtml(btns.InnerHtml);
                }
            }
            else if (PageModel.CurrentPage == PageModel.TotalPages)
            {
                if (PageModel.TotalPages > 1)
                {
                    TagBuilder result = LastPage();
                    TagBuilder btns = AddNavBtnsPrev();
                    output.Content.AppendHtml(btns.InnerHtml);
                    output.Content.AppendHtml(result.InnerHtml);
                }
            }
            else
            {
                if(PageModel.TotalPages > 1)
                {
                    TagBuilder btns = AddNavBtnsNext();
                    TagBuilder btnsF = AddNavBtnsPrev();
                    TagBuilder result = OtherPage();
                    output.Content.AppendHtml(btnsF.InnerHtml);
                    output.Content.AppendHtml(result.InnerHtml);
                    output.Content.AppendHtml(btns.InnerHtml);
                }
            }


        }
        public TagBuilder FirstPage()
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            int numOfPages = PageModel.TotalPages;
            if (numOfPages > 2)
            {
                numOfPages = 3;
            }
            for (int i = 1; i <= numOfPages; i++)
            {
                    int n = i;
                    string s = i.ToString();
                    TagBuilder tag = new TagBuilder("a");
                    PageUrlValues["page"] = n;
                    tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    tag.InnerHtml.Append(s);
                    result.InnerHtml.AppendHtml(tag);
            }
            return result;
        }
        public TagBuilder LastPage()
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            int numOfPages = PageModel.TotalPages - 2;
            if (numOfPages < 1)
            {
                numOfPages = 1;
            }
            for (int i = numOfPages; i <= PageModel.CurrentPage + 2; i++)
            {
                if (i <= PageModel.TotalPages)
                {
                        int n = i;
                        string s = i.ToString();
                        TagBuilder tag = new TagBuilder("a");
                        PageUrlValues["page"] = n;
                        tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                        tag.InnerHtml.Append(s);
                        result.InnerHtml.AppendHtml(tag);
                }
            }
            return result;
        }
        public TagBuilder OtherPage()
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            int numOfPages = PageModel.CurrentPage - 1;
            if (numOfPages < 1)
            {
                numOfPages = 1;
            }
            for (int i = numOfPages; i <= PageModel.CurrentPage + 1; i++)
            {
                if (i <= PageModel.TotalPages)
                {
                    int n = i;
                    string s = i.ToString();
                    TagBuilder tag = new TagBuilder("a");
                    PageUrlValues["page"] = n;
                    tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                    tag.InnerHtml.Append(s);
                    result.InnerHtml.AppendHtml(tag);
                }

            }
            return result;
        }
        public TagBuilder AddNavBtnsPrev()
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            TagBuilder tag2 = new TagBuilder("a");
            PageUrlValues["page"] = 1;
            tag2.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            tag2.InnerHtml.Append("First");
            result.InnerHtml.AppendHtml(tag2);

            TagBuilder tag = new TagBuilder("a");
            PageUrlValues["page"] = PageModel.CurrentPage - 1;
            tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            tag.InnerHtml.Append("Previous");
            result.InnerHtml.AppendHtml(tag);

            return result;
        }
        public TagBuilder AddNavBtnsNext()
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            TagBuilder tag = new TagBuilder("a");
            PageUrlValues["page"] = PageModel.CurrentPage + 1;
            tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            tag.InnerHtml.Append("Next");
            result.InnerHtml.AppendHtml(tag);

            TagBuilder tag2 = new TagBuilder("a");
            PageUrlValues["page"] = PageModel.TotalPages;
            tag2.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
            tag2.InnerHtml.Append("Last");
            result.InnerHtml.AppendHtml(tag2);

            return result;
        }
    }
}
