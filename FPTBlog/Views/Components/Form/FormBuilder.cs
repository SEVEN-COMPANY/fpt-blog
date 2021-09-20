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

        public IHtmlContent LoadingWave()
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.MergeAttribute("id", "loading");
            wrapperComponent.AddCssClass(" items-center hidden justify-center space-x-2 fade-in");
            TagBuilder loadingLine1 = new TagBuilder("div");
            loadingLine1.AddCssClass("w-2 h-12 bg-tango-500 animation1");
            TagBuilder loadingLine2 = new TagBuilder("div");
            loadingLine2.AddCssClass("w-2 h-12 bg-tango-500 animation2");

            wrapperComponent.InnerHtml += loadingLine1.ToString(TagRenderMode.Normal) +
            loadingLine2.ToString(TagRenderMode.Normal) +
            loadingLine1.ToString(TagRenderMode.Normal) +
            loadingLine2.ToString(TagRenderMode.Normal) +
            loadingLine1.ToString(TagRenderMode.Normal);

            return new HtmlString(wrapperComponent.ToString(TagRenderMode.Normal));
        }

        public IHtmlContent FieldMessage(string field)
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.AddCssClass("font-semibold ");

            TagBuilder messageComponent = new TagBuilder("p");
            var message = (string)this.viewData["message"] ?? "";
            messageComponent.AddCssClass("text-green-500 first-letter");
            messageComponent.MergeAttribute("id", $"{field.ToUpper()}ERROR");
            messageComponent.SetInnerText(message);

            return new HtmlString(wrapperComponent.ToString(TagRenderMode.Normal));

        }
        public IHtmlContent Message()
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.AddCssClass("font-semibold ");

            TagBuilder messageComponent = new TagBuilder("p");
            var message = (string)this.viewData["message"] ?? "";
            messageComponent.AddCssClass("text-green-500 first-letter");
            messageComponent.MergeAttribute("id", "MESSAGEERROR");
            messageComponent.SetInnerText(message);

            TagBuilder errorComponent = new TagBuilder("p");
            var errorMessage = (string)this.viewData["errorMessage"] ?? "";
            errorComponent.AddCssClass("text-red-500 first-letter");
            errorComponent.MergeAttribute("id", "ERRORMESSAGEERROR");
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
            input.MergeAttribute("id", name);
            input.MergeAttribute("type", type);
            input.MergeAttribute("value", value);
            return input.ToString(TagRenderMode.SelfClosing);
        }

        public IHtmlContent RadioButton(string name, string label)
        {
            TagBuilder wrapperComponent = new TagBuilder("div");
            wrapperComponent.AddCssClass("flex items-center space-x-2 check-box");
            var value = (Microsoft.AspNetCore.Mvc.Rendering.SelectList)this.viewData[name];
            var list = value.Items;
            foreach (Microsoft.AspNetCore.Mvc.Rendering.SelectListItem item in list)
            {

                TagBuilder divComponent = new TagBuilder("div");
                divComponent.AddCssClass("flex items-center ");
                TagBuilder labelComponent = new TagBuilder("label");
                labelComponent.AddCssClass("inline-block w-5 h-5 duration-300 mr-1 border-2 rounded-full border-tango-500");
                labelComponent.MergeAttribute("for", item.Text);

                TagBuilder spanComponent = new TagBuilder("span");
                spanComponent.AddCssClass("first-letter");
                spanComponent.SetInnerText(item.Text);

                TagBuilder inputComponent = new TagBuilder("input");
                inputComponent.AddCssClass("hidden");
                inputComponent.MergeAttribute("type", "radio");
                inputComponent.MergeAttribute("name", name);
                inputComponent.MergeAttribute("id", item.Text);
                inputComponent.MergeAttribute("value", item.Value);


                if ((string)value.SelectedValue == item.Value)
                {
                    inputComponent.MergeAttribute("checked", "checked");
                }
                divComponent.InnerHtml += inputComponent.ToString(TagRenderMode.SelfClosing) + labelComponent.ToString(TagRenderMode.Normal) + spanComponent.ToString(TagRenderMode.Normal);
                wrapperComponent.InnerHtml += divComponent.ToString(TagRenderMode.Normal);
            }



            var component = this.FieldWrapper(name, label, wrapperComponent.ToString(TagRenderMode.Normal));
            return new HtmlString(component);
        }


        private string FieldWrapper(string name, string label, string componentInside)
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


        public IHtmlContent Button(string label, string type = "submit")
        {
            TagBuilder wrapper = new TagBuilder("div");

            TagBuilder button = new TagBuilder("button");
            button.MergeAttribute("id", "form-btn");
            button.MergeAttribute("type", type);
            button.SetInnerText(label);
            button.AddCssClass("w-full p-2 font-semibold text-white duration-300 bg-blue-600 hover:bg-blue-800 focus:outline-none");

            wrapper.InnerHtml += button.ToString(TagRenderMode.Normal);
            wrapper.InnerHtml += this.LoadingWave();
            return new HtmlString(wrapper.ToString(TagRenderMode.Normal));
        }

        public IHtmlContent SelectDropList(string name, string label)
        {
            TagBuilder wrapperComponent = new TagBuilder("select");
            wrapperComponent.MergeAttribute("name", name);
            wrapperComponent.MergeAttribute("id", name);
            wrapperComponent.AddCssClass("block w-full px-2 py-1 duration-300 border outline-none h-9 focus:border-tango-500 safari");

            var value = (Microsoft.AspNetCore.Mvc.Rendering.SelectList)this.viewData[name];
            var list = value.Items;

            foreach (Microsoft.AspNetCore.Mvc.Rendering.SelectListItem item in list)
            {

                TagBuilder optionComponent = new TagBuilder("option");


                optionComponent.SetInnerText(item.Text);
                optionComponent.MergeAttribute("value", item.Value);


                if ((string)value.SelectedValue == item.Value)
                {
                    optionComponent.MergeAttribute("selected", "selected");
                }
                wrapperComponent.InnerHtml += optionComponent.ToString(TagRenderMode.Normal);
            }

            var component = this.FieldWrapper(name, label, wrapperComponent.ToString(TagRenderMode.Normal));
            return new HtmlString(component);
        }

        public IHtmlContent FormWrapper(string title, string formId, string submitButtonLabel, IHtmlContent[] fields)
        {
            TagBuilder formWrapper = new TagBuilder("form");
            formWrapper.AddCssClass("space-y-6");
            formWrapper.MergeAttribute("method", "POST");
            formWrapper.MergeAttribute("id", formId);

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