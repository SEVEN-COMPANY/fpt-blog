using System.Linq;
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
using System;

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

        [HttpGet("tag")]
        public IActionResult GetTagsByBlogId(string blogId) {
            var res = new ServerApiResponse<List<Tag>>();

            Blog blog = this.BlogService.GetBlogByBlogId(blogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND);
                return new NotFoundObjectResult(res.getResponse());
            }

            var tags = this.BlogService.GetTagsFromBlog(blog);
            res.data = tags;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("tag")]
        public IActionResult AddTagToBlog([FromBody] ToggleTagToBlogDto input) {
            var res = new ServerApiResponse<List<Tag>>();
            ValidationResult result = new ToggleTagToBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            // Find tag by name in db, if not found, then create new
            Tag tag = this.TagService.GetTagByName(input.TagName);
            if(tag == null){
                tag = new Tag();
                tag.Name = input.TagName;
                tag.Status = TagStatus.ACTIVE;
                this.TagService.SaveTag(tag);
            }

            List<Tag> tags = this.BlogService.GetTagsFromBlog(blog);
            // If the current tags of blog already has this tag name, return
            foreach(var item in tags){
                if(item.Name.Equals(tag.Name)){
                    res.data = tags;
                    res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
                    return new ObjectResult(res.getResponse());
                }
            }

            this.BlogService.AddTagToBlog(blog, tag);
            tags.Add(tag);
            res.data = tags;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("tag")]
        public IActionResult RemoveTagFromBlog([FromBody] ToggleTagToBlogDto input){
             var res = new ServerApiResponse<List<Tag>>();
            ValidationResult result = new ToggleTagToBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
                return new NotFoundObjectResult(res.getResponse());
            }

            Tag tag = this.TagService.GetTagByName(input.TagName);
            if(tag == null){
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "tagName");
                return new NotFoundObjectResult(res.getResponse());
            }

            List<Tag> tags = this.BlogService.GetTagsFromBlog(blog);
            bool canRemove = false;
            foreach(var item in tags){
                if(item.Name.Equals(tag.Name)){
                    canRemove = true;
                }
            }

            if(canRemove){
                tags.Remove(tag);
                this.BlogService.RemoveTagFromBlog(blog, tag);
            }

            res.data = tags;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
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
