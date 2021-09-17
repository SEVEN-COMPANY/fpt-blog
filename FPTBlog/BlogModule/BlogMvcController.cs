using System;
using FluentValidation.Results;
using FPTBlog.AuthModule;
using FPTBlog.BlogModule.DTO;
using FPTBlog.BlogModule.Entity;
using FPTBlog.BlogModule.Interface;
using FPTBlog.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.BlogModule
{
    [Route("blog")]
<<<<<<< HEAD:FPTBlog/BlogModule/BlogMvcController.cs
    public class BlogMvcController : Controller
=======
    [ServiceFilter(typeof(AuthGuard))]
    public class BlogController : Controller
>>>>>>> 1d09fb12ce8f0288164d657b143877db7223dad4:FPTBlog/BlogModule/BlogController.cs
    {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        public BlogMvcController(IUploadFileService uploadFileService, IBlogService blogService)
        {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
        }

        [HttpGet("")]
        public IActionResult EditorPage()
        {
            Blog blog = new Blog();
            User currentUser =  (User)this.ViewData["user"];
            blog.Student = currentUser;
            blog.StudentId = currentUser.UserId;
            ViewData["blog"] = blog;
            this.BlogService.SaveBlog(blog);
            return View(Routers.EditorPage.Page);
        }

        [HttpPost("image")]
        public string UploadImageHandler(IFormFile input)
        {
            if (this.UploadFileService.CheckFileSize(input, 500))
            {
                return null;
            }

            if (this.UploadFileService.CheckFileExtension(input, new string[] { "png", "jpeg", "gif", "tiff" }))
            {
                return null;
            }

            return this.UploadFileService.Upload(input);
        }

        [HttpPost("save")]
        public string HandleOnSave([FromBody] SaveBlogDto input)
        {
            ValidationResult result = new SaveBlogDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                return "not pass validation";
            }
            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null)
            {

                return "blog not found";
            }
            blog.Title = input.Title;
            blog.Content = input.Content;
            this.BlogService.UpdateBlog(blog);

            return "ok";
        }


        [HttpPost("")]
        public IActionResult AddBlogHandler([FromBody] AddBlogDto input)
        {
            ValidationResult result = new AddBlogDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.EditorPage.Page);
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null)
            {
                ServerResponse.SetFieldErrorMessage("blogId", CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return View(Routers.EditorPage.Page);
            }

            blog.Title = input.Title;
            blog.Content = input.Content;
            this.BlogService.UpdateBlog(blog);

            return View(Routers.GetBlog.Page);
        }
    }
}