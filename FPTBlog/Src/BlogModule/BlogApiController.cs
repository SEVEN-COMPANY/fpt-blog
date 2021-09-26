
using System;
using System.Collections.Generic;
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


namespace FPTBlog.Src.BlogModule {
    [Route("/api/blog")]
    [ServiceFilter(typeof(AuthGuard))]
    public class BlogApiController : Controller {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        private readonly ICategoryService CategoryService;
        private readonly ITagService TagService;
        public BlogApiController(IUploadFileService uploadFileService, IBlogService blogService, ICategoryService categoryService, ITagService tagService) {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
            this.CategoryService = categoryService;
            this.TagService = tagService;
        }

        [HttpPost("image")]
        public IActionResult UploadImageHandler(IFormFile image) {

            var res = new ServerApiResponse<string>();

            if (!this.UploadFileService.CheckFileSize(image, 5)) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE);
                return new BadRequestObjectResult(res.getResponse());
            }

            if (!this.UploadFileService.CheckFileExtension(image, new string[] { "jpg", "png", "jpeg", "gif", "tiff" })) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION);
                return new BadRequestObjectResult(res.getResponse());
            }


            res.data = this.UploadFileService.Upload(image);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("")]
        public IActionResult SaveBlogHandler() {
            Blog blog = new Blog();
            blog.Student = (User) this.ViewData["user"];
            blog.StudentId = ((User) this.ViewData["user"]).UserId;
            this.BlogService.SaveBlog(blog);
            var res = new ServerApiResponse<Blog>();
            res.data = blog;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("save")]
        public IActionResult SaveBlogHandler([FromBody] SaveBlogDto input) {
            var res = new ServerApiResponse<Blog>();
            ValidationResult result = new SaveBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            User student = (User) this.ViewData["user"];

            blog.Title = input.Title;
            blog.Content = input.Content;
            blog.Student = student;
            blog.StudentId = student.UserId;

            this.BlogService.UpdateBlog(blog);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_SAVE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("category")]
        public IActionResult AddCategoryToBlog([FromBody] UpdateCategoryOfBlogDto input) {
            var res = new ServerApiResponse<Blog>();
            ValidationResult result = new UpdateCategoryOfBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            Category category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "categoryId");
                return new NotFoundObjectResult(res.getResponse());
            }

            blog.CategoryId = category.CategoryId;
            blog.Category = category;

            this.BlogService.UpdateBlog(blog);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("tag")]
        public IActionResult AddTagToBlog([FromBody] UpdateTagsOfBlogDto input) {
            var res = new ServerApiResponse<Blog>();
            ValidationResult result = new UpdateTagsOfBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            List<Tag> currentTags = this.BlogService.GetTagFromBlog(blog);
            List<Tag> newTags = new List<Tag>();
            foreach (string tagId in input.Tags) {
                Tag tag = this.TagService.GetTagByTagId(tagId);
                newTags.Add(tag);
            }

            // Thêm những tag mà người dùng vừa thêm mới
            List<Tag> addTags = new List<Tag>();
            foreach (Tag newTag in newTags) {
                if (!currentTags.Contains(newTag)) {
                    addTags.Add(newTag);
                }
            }
            this.BlogService.AddTagToBlog(blog, addTags);

            // Xóa những tag mà người dùng đã remove ra khỏi blog
            List<Tag> removeTags = new List<Tag>();
            foreach (Tag curTag in currentTags) {
                if (!newTags.Contains(curTag)) {
                    removeTags.Add(curTag);
                }
            }
            this.BlogService.RemoveTagFromBlog(removeTags);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("post")]
        public IActionResult PostBlog([FromBody] PostBlogDto input) {
            var res = new ServerApiResponse<Blog>();
            ValidationResult result = new PostBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            blog.Status = BlogStatus.WAIT;
            this.BlogService.UpdateBlog(blog);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_POSTED_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}
