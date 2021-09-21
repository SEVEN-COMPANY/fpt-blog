
using System;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.DTO;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.Src.BlogModule
{
    [Route("/api/blog")]
    [ServiceFilter(typeof(AuthGuard))]
    public class BlogApiController : Controller
    {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        private readonly ICategoryService CategoryService;
        private readonly ITagService TagService;
        public BlogApiController(IUploadFileService uploadFileService, IBlogService blogService, ICategoryService categoryService, ITagService tagService)
        {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
            this.CategoryService = categoryService;
            this.TagService = tagService;
        }

        [HttpPost("image")]
        public IActionResult UploadImageHandler(IFormFile image)
        {

            var res = new ServerApiResponse<string>();

            if (!this.UploadFileService.CheckFileSize(image, 5))
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE);
                return new BadRequestObjectResult(res.getResponse());
            }

            if (!this.UploadFileService.CheckFileExtension(image, new string[] { "jpg", "png", "jpeg", "gif", "tiff" }))
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION);
                return new BadRequestObjectResult(res.getResponse());
            }


            res.data = this.UploadFileService.Upload(image);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("")]
        public IActionResult SaveBlogHandler(){
            Blog blog = new Blog();
            this.BlogService.SaveBlog(blog);
            var res = new ServerApiResponse<Blog>();
            res.data = blog;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("save")]
        public IActionResult SaveBlogHandler([FromBody] SaveBlogDto input)
        {
            var res = new ServerApiResponse<Blog>();
            ValidationResult result = new SaveBlogDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null)
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            User student = (User)this.ViewData["user"];

            blog.Title = input.Title;
            blog.Content = input.Content;
            blog.Student = student;
            blog.StudentId = student.UserId;

            this.BlogService.UpdateBlog(blog);
            
            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_SAVE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}