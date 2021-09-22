using System.Collections.Generic;

using System;



using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;

namespace FPTBlog.Views.Components.Dashboard
{
    public class DashboardBuilder
    {
        private string SideMenuLinkItem(string label, string link)
        {

            TagBuilder listItem = new TagBuilder("li");
            TagBuilder linkItem = new TagBuilder("a");
            linkItem.SetInnerText(label);
            linkItem.MergeAttribute("href", link);
            linkItem.AddCssClass("block duration-300 hover:text-white");
            listItem.InnerHtml += linkItem.ToString(TagRenderMode.Normal);

            return listItem.ToString(TagRenderMode.Normal);
        }


        private string ArrowGroupBtn(int index)
        {
            TagBuilder btnArrowMenu = new TagBuilder("div");
            btnArrowMenu.MergeAttribute("id", $"menu-arrow-{index}");
            btnArrowMenu.AddCssClass("duration-300 transform");

            TagBuilder btnArrowImage = new TagBuilder("img");
            btnArrowImage.MergeAttribute("src", "/svg/arrow-icon.svg");
            btnArrowImage.MergeAttribute("alt", "arrow");

            btnArrowMenu.InnerHtml = btnArrowImage.ToString(TagRenderMode.SelfClosing);

            return btnArrowMenu.ToString(TagRenderMode.Normal);
        }

        public HtmlString SideMenuGroup(string label, string iconUrl, int index, List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> list)
        {
            TagBuilder menuGroup = new TagBuilder("div");
            menuGroup.AddCssClass("h-auto space-y-1 overflow-hidden admin-dashboard ");
            menuGroup.MergeAttribute("id", $"menu-container-{index}");

            TagBuilder btnMenu = new TagBuilder("div");
            btnMenu.AddCssClass("flex items-center justify-between py-2 pl-6 pr-4 space-x-2 text-white rounded-lg cursor-pointer bg-persian-blue-500");
            btnMenu.MergeAttribute("id", $"menu-btn-{index}");




            TagBuilder btnLabelMenu = new TagBuilder("div");
            btnLabelMenu.AddCssClass("flex items-center space-x-2");

            TagBuilder btnLabelText = new TagBuilder("span");
            btnLabelText.AddCssClass("inline-block font-semibold");
            btnLabelText.SetInnerText(label);

            TagBuilder btnLabelIcon = new TagBuilder("img");
            btnLabelIcon.MergeAttribute("src", iconUrl);
            btnLabelIcon.MergeAttribute("alt", label);

            btnLabelMenu.InnerHtml += btnLabelIcon.ToString(TagRenderMode.SelfClosing) + btnLabelText.ToString(TagRenderMode.Normal);




            btnMenu.InnerHtml += btnLabelMenu.ToString(TagRenderMode.Normal) + this.ArrowGroupBtn(index);

            TagBuilder listMenu = new TagBuilder("ul");
            listMenu.AddCssClass("flex-grow-0 w-full py-2 pl-8 space-y-2 font-medium text-gray-400 duration-300 origin-top transform scale-y-0 rounded-lg bg-big-stone-400");
            listMenu.MergeAttribute("id", $"menu-list-{index}");
            foreach (var item in list)
            {
                listMenu.InnerHtml += this.SideMenuLinkItem(item.Text, item.Value);
            }


            menuGroup.InnerHtml = btnMenu.ToString(TagRenderMode.Normal) + listMenu.ToString(TagRenderMode.Normal);

            return new HtmlString(menuGroup.ToString(TagRenderMode.Normal));
        }
    }
}