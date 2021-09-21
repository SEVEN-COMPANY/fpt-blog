using System;
using System.Collections.Generic;
using backend.Src.AuthModule;
using FluentValidation;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.TagModule.DTO;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.TagModule
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

        [HttpGet("update")]
        public IActionResult UpdateTagPage(string tagId)
        {
            Tag tag = this.TagService.GetTagByTagId(tagId);
            if (tag == null)
            {
                ServerMvcResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return View(Routers.UpdateTag.Page);
            }
            ViewData["tag"] = tag;
            return View(Routers.UpdateTag.Page);
        }

        [HttpGet("")]
        public IActionResult GetTagsPage()
        {
            List<Tag> tags = this.TagService.GetTags();
            this.ViewData["tags"] = tags;

            return View(Routers.GetTags.Page);
        }
    }
}