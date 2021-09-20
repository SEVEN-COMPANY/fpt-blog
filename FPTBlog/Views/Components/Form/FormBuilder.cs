using System;



using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;


namespace FPTBlog.Views.Components.Form
{
    public class FormBuilder
    {
        HttpContext Context;
        Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary viewData;
        public FormBuilder(HttpContext context, Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary viewData)
        {
            this.Context = context;
            this.viewData = viewData;
        }


        public IHtmlContent Message()
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.AddCssClass("font-semibold ");

            TagBuilder messageComponent = new TagBuilder("p");
            var message = (string)this.viewData["message"] ?? "";
            messageComponent.AddCssClass("text-green-500 first-letter");
            messageComponent.SetInnerText(message);

            TagBuilder errorComponent = new TagBuilder("p");
            var errorMessage = (string)this.viewData["errorMessage"] ?? "";
            errorComponent.AddCssClass("text-red-500 first-letter");
            errorComponent.SetInnerText(errorMessage);


            wrapperComponent.InnerHtml += messageComponent.ToString(TagRenderMode.Normal) + errorComponent.ToString(TagRenderMode.Normal);
            return new HtmlString(wrapperComponent.ToString(TagRenderMode.Normal));
        }

        private string TextFieldCore(string name, string type)
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
            input.MergeAttribute("type", type);
            input.MergeAttribute("value", value);
            return input.ToString(TagRenderMode.SelfClosing);
        }

        private string FieldWrapper(string name, string label, string componentInside)
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.AddCssClass("flex flex-col flex-1 space-y-1");


            TagBuilder labelComponent = new TagBuilder("label");
            labelComponent.AddCssClass("block font-semibold capitalize");
            labelComponent.SetInnerText(label);


            TagBuilder errorComponent = new TagBuilder("span");
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

        public IHtmlContent TextField(string name, string label)
        {
            var input = this.TextFieldCore(name, "text");
            var component = this.FieldWrapper(name, label, input);
            return new HtmlString(component);
        }
        public IHtmlContent PasswordField(string name, string label)
        {
            var input = this.TextFieldCore(name, "password");
            var component = this.FieldWrapper(name, label, input);
            return new HtmlString(component);
        }


        public IHtmlContent Button(string label, string type)
        {
            TagBuilder wrapper = new TagBuilder("div");
            wrapper.AddCssClass("bg-red-500");

            TagBuilder button = new TagBuilder("button");
            button.MergeAttribute("type", type);
            button.SetInnerText(label);
            button.AddCssClass("w-full p-2 font-semibold text-white duration-300 bg-blue-600 hover:bg-blue-800 focus:outline-none");

            wrapper.InnerHtml += button.ToString(TagRenderMode.Normal);
            return new HtmlString(wrapper.ToString(TagRenderMode.Normal));
        }

        public IHtmlContent FormWrapper(string title, string action, string submitButtonLabel, IHtmlContent[] fields)
        {
            TagBuilder formWrapper = new TagBuilder("form");
            formWrapper.AddCssClass("space-y-6");
            formWrapper.MergeAttribute("method", "POST");
            formWrapper.MergeAttribute("action", action);
            TagBuilder formTitle = new TagBuilder("h1");
            formTitle.AddCssClass("text-4xl font-semibold text-center");
            formTitle.SetInnerText(title);
            formWrapper.InnerHtml += formTitle.ToString(TagRenderMode.Normal);
            TagBuilder filedWrapper = new TagBuilder("div");
            filedWrapper.AddCssClass("space-y-4");
            filedWrapper.InnerHtml += this.Message().ToString();
            foreach (var item in fields)
            {
                filedWrapper.InnerHtml += item.ToString();
            }
            filedWrapper.InnerHtml += this.Button(submitButtonLabel, "submit").ToString();

            formWrapper.InnerHtml += filedWrapper.ToString(TagRenderMode.Normal);
            return new HtmlString(formWrapper.ToString(TagRenderMode.Normal));

        }

    }
}