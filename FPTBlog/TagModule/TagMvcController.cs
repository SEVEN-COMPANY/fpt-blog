using System;
using System.Collections.Generic;
using backend.Src.AuthModule;
using FluentValidation;
using FluentValidation.Results;
using FPTBlog.AuthModule;
using FPTBlog.TagModule.DTO;
using FPTBlog.TagModule.Entity;
using FPTBlog.TagModule.Interface;
using FPTBlog.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.TagModule
{
    [Route("tag")]
    public class TagMvcController : Controller
    {
        private readonly ITagService TagService;
        public TagMvcController(ITagService tagService)
        {
            this.TagService = tagService;
        }

        [HttpGet("add")]
        public IActionResult AddTagPage()
        {
            return View(Routers.AddTag.Page);
        }

        [HttpPost("add")]
        public IActionResult AddTagHandler(string name)
        {
            var input = new AddTagDto()
            {
                Name = name
            };

            ValidationResult result = new AddTagDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.AddTag.Page);
            }

            var existedTag = this.TagService.GetTagByName(input.Name);
            if (existedTag != null)
            {
                ServerResponse.SetFieldErrorMessage("name", CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, this.ViewData);
                return View(Routers.AddTag.Page);
            }

            Tag tag = new Tag();
            tag.Name = input.Name;
            this.TagService.SaveTag(tag);

            ServerResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS, this.ViewData);
            return View(Routers.AddTag.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateTagPage(string tagId)
        {
            Tag tag = this.TagService.GetTagByTagId(tagId);
            if (tag == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return View(Routers.UpdateTag.Page);
            }
            ViewData["tag"] = tag;
            return View(Routers.UpdateTag.Page);
        }

        [HttpPost("update")]
        public IActionResult UpdateTagHandler(string tagId, string name)
        {
            var input = new UpdateTagDto()
            {
                TagId = tagId,
                Name = name
            };

            var tag = this.TagService.GetTagByTagId(input.TagId);
            if (tag == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return View(Routers.UpdateTag.Page);
            }
            this.ViewData["tag"] = tag;

            ValidationResult result = new UpdateTagDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.UpdateTag.Page);
            }

            tag.Name = input.Name;
            this.TagService.UpdateTag(tag);

            ServerResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS, this.ViewData);
            return View(Routers.UpdateTag.Page);
        }

        [HttpGet("")]
        public IActionResult GetTagsPage()
        {
            List<Tag> tags = this.TagService.GetTags();
            this.ViewData["tags"] = tags;

            return View(Routers.GetTags.Page);
        }

        [HttpGet("delete")]
        public IActionResult DeleteTagHandler(string tagId)
        {
            Tag tag = this.TagService.GetTagByTagId(tagId);
            if (tag == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return View(Routers.GetTags.Page);
            }
            this.TagService.DeleteTag(tagId);

            var message = ValidatorOptions.Global.LanguageManager.GetString(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
            return Redirect($"{Routers.GetTags.Link}?message={message}");
        }
    }
}