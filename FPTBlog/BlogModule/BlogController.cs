using System;
using FluentValidation.Results;
using FPTBlog.BlogModule.DTO;
using FPTBlog.BlogModule.Entity;
using FPTBlog.BlogModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.BlogModule
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        public BlogController(IUploadFileService uploadFileService, IBlogService blogService){
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
        }

        [HttpGet("")]
        public IActionResult EditorPage()
        {
            Blog blog = new Blog();
            ViewData["blog"] = blog;
            return View(Routers.EditorPage.Page);
        }

        [HttpPost("image")]
        public string UploadImageHandler(IFormFile input)
        {
            if(this.UploadFileService.CheckFileSize(input, 500)){
                return null;
            }

            if(this.UploadFileService.CheckFileExtension(input,new string[]{"png", "jpeg", "gif", "tiff"})){
                return null;
            }

            return this.UploadFileService.Upload(input);
        }

        [HttpPost("save")]
        public string HandleOnSave([FromBody]SaveBlogDto input)
        {
            ValidationResult result = new SaveBlogDtoValidator().Validate(input);
            if(!result.IsValid)
            {
                return "not pass validation";
            }
            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);

            return "ok";
        }


        [HttpPost("")]
        public IActionResult AddBlogHandler(string title, string contain)
        {
            Console.WriteLine(title);
            Console.WriteLine(contain);
            // Console.WriteLine(editor);
            return View(Routers.AddBlog.Page);
        }
    }
}