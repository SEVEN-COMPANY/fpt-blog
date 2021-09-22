using System.Collections.Generic;

using System;



using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;


namespace FPTBlog.Views.Components.Table
{
    public class TableBuilder
    {

        public IHtmlContent HeaderCol(string content)
        {
            TagBuilder Col = new TagBuilder("th");
            Col.AddCssClass("px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase");
            Col.MergeAttribute("scope", "col");
            Col.SetInnerText(content);

            return new HtmlString(Col.ToString(TagRenderMode.Normal));
        }
        public IHtmlContent BodyColBadge(string content, string status)
        {
            TagBuilder Col = new TagBuilder("td");
            Col.AddCssClass("px-6 py-4 whitespace-nowrap");
            TagBuilder badge = new TagBuilder("span");
            badge.AddCssClass("inline-flex px-2 text-xs font-semibold leading-5  rounded-full");
            if (status == "green")
            {
                badge.AddCssClass("text-green-800 bg-green-100");
            }
            else if (status == "red")
            {
                badge.AddCssClass("text-red-500 bg-red-100");

            }



            badge.SetInnerText(content);
            Col.InnerHtml += badge.ToString(TagRenderMode.Normal);
            return new HtmlString(Col.ToString(TagRenderMode.Normal));
        }
        public IHtmlContent BodyColLink(string label, string url, string color)
        {
            TagBuilder Col = new TagBuilder("td");
            Col.AddCssClass("px-6 py-4 text-sm font-medium text-right whitespace-nowrap");
            TagBuilder link = new TagBuilder("a");
            link.MergeAttribute("href", url);
            if (color == "indigo")
            {
                link.AddCssClass("text-indigo-600 hover:text-indigo-900");
            }

            link.SetInnerText(label);
            Col.InnerHtml += link.ToString(TagRenderMode.Normal);
            return new HtmlString(Col.ToString(TagRenderMode.Normal));
        }

        public IHtmlContent BodyCol(string content)
        {
            TagBuilder Col = new TagBuilder("td");
            Col.AddCssClass("px-6 py-4 whitespace-nowrap");
            Col.SetInnerText(content);

            return new HtmlString(Col.ToString(TagRenderMode.Normal));
        }
        public IHtmlContent Row(IHtmlContent[] cols)
        {
            TagBuilder body = new TagBuilder("tr");
            foreach (var item in cols)
            {
                body.InnerHtml += item.ToString();
            }


            return new HtmlString(body.ToString(TagRenderMode.Normal));
        }
        public IHtmlContent TableBody(List<IHtmlContent> rows)
        {
            TagBuilder body = new TagBuilder("tbody");
            body.AddCssClass("bg-white divide-y divide-gray-200");
            foreach (var item in rows)
            {
                body.InnerHtml += item.ToString();
            }


            return new HtmlString(body.ToString(TagRenderMode.Normal));
        }
        public IHtmlContent TableHead(IHtmlContent[] rows)
        {
            TagBuilder body = new TagBuilder("thead");
            body.AddCssClass("bg-gray-50");
            foreach (var item in rows)
            {
                body.InnerHtml += item.ToString();
            }


            return new HtmlString(body.ToString(TagRenderMode.Normal));
        }

        public IHtmlContent Table(IHtmlContent[] cols)
        {
            TagBuilder table = new TagBuilder("table");
            table.AddCssClass("min-w-full divide-y divide-gray-200");
            foreach (var item in cols)
            {
                table.InnerHtml += item.ToString();
            }


            return new HtmlString(table.ToString(TagRenderMode.Normal));
        }


    }
}