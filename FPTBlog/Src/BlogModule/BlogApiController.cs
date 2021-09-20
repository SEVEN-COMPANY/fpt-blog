using System;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.DTO;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
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
        public BlogApiController(IUploadFileService uploadFileService, IBlogService blogService)
        {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
        }

        [HttpPost("image")]
        public IActionResult UploadImageHandler(IFormFile input)
        {
            var res = new ServerApiResponse<string>();

            if (this.UploadFileService.CheckFileSize(input, 500))
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE);
                return new BadRequestObjectResult(res.getResponse());
            }

            if (this.UploadFileService.CheckFileExtension(input, new string[] { "png", "jpeg", "gif", "tiff" }))
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE);
                return new BadRequestObjectResult(res.getResponse());
            }


            res.data = this.UploadFileService.Upload(input);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("save")]
        public IActionResult HandleOnSave([FromBody] SaveBlogDto input)
        {
            var res = new ServerApiResponse<string>();
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
            blog.Title = input.Title;
            blog.Content = input.Content;
            this.BlogService.UpdateBlog(blog);

            return new ObjectResult(res.getResponse());
        }


        [HttpPost("")]
        public IActionResult AddBlogHandler([FromBody] AddBlogDto input)
        {
            var res = new ServerApiResponse<string>();
            ValidationResult result = new AddBlogDtoValidator().Validate(input);
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

            blog.Title = input.Title;
            blog.Content = input.Content;
            this.BlogService.UpdateBlog(blog);

            return new ObjectResult(res.getResponse());
        }
    }
}