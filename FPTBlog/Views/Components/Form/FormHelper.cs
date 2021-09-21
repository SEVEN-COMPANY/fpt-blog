
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;


namespace FPTBlog.Views.Components.Form
{
    public class FormHelper
    {
        HttpContext Context;
        Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary viewData;
        public FormHelper(HttpContext context, Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary viewData)
        {
            this.Context = context;
            this.viewData = viewData;
        }


        protected string TextFieldCore(string name, string type)
        {
            var value = "";
            try
            {
                value = (string)this.Context.Request.Form[name];
            }
            catch (System.Exception)
            {

            }


            TagBuilder input = new TagBuilder("input");
            input.AddCssClass("block w-full px-2 py-1 duration-300 border safari rounded-sm outline-none h-9 focus:border-tango-500");
            input.MergeAttribute("name", name);
            input.MergeAttribute("id", name);
            input.MergeAttribute("type", type);
            input.MergeAttribute("value", value);
            return input.ToString(TagRenderMode.SelfClosing);
        }

        protected string FieldWrapper(string name, string label, string componentInside)
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.AddCssClass("flex flex-col flex-1 space-y-1");


            TagBuilder labelComponent = new TagBuilder("label");
            labelComponent.AddCssClass("block font-semibold capitalize");
            labelComponent.SetInnerText(label);
            labelComponent.MergeAttribute("id", $"{name.ToUpper()}LABEL");


            TagBuilder errorComponent = new TagBuilder("span");
            errorComponent.MergeAttribute("id", $"{name.ToUpper()}ERROR");
            errorComponent.AddCssClass("text-red-500 fade-in block");

            var errorMessage = (string)this.viewData[$"{name}Error"];
            if (errorMessage != null)
            {
                var formatErrorMessage = label + " " + errorMessage;
                errorComponent.SetInnerText(formatErrorMessage);

            }


            wrapperComponent.InnerHtml += labelComponent.ToString(TagRenderMode.Normal) + componentInside + errorComponent.ToString(TagRenderMode.Normal);
            return wrapperComponent.ToString(TagRenderMode.Normal);
        }
    }
}