using System;
using FluentValidation.Results;
using FPTBlog.TagModule.DTO;
using FPTBlog.TagModule.Entity;
using FPTBlog.TagModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.TagModule
{
    [Route("tag")]
    public class TagController: Controller
    {
        private readonly ITagService TagService;
        public TagController(ITagService tagService){
            this.TagService = tagService;
        }

        [HttpGet("add")]
        public IActionResult AddTagPage()
        {
            Console.WriteLine("get");
            return View(Routers.AddTag.Page);
        }

        [HttpPost("add")]
        public IActionResult AddTagHandler(string name)
        {
            Console.WriteLine("post");
            var input = new AddTagDto(){
                Name = name
            };

            ValidationResult result = new AddTagDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.AddTag.Page);
            }

            var existedTag = this.TagService.GetTagByName(input.Name);
            if(existedTag != null){
                ServerResponse.SetFieldErrorMessage("name", CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, this.ViewData);
                return View(Routers.AddTag.Page);
            }

            Tag tag = new Tag();
            tag.Name = input.Name;
            this.TagService.SaveTag(tag);

            ServerResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS, this.ViewData);
            return View(Routers.AddTag.Page);
        }
    }
}