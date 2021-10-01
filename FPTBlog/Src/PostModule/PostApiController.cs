using System.Linq;
using System.Collections.Generic;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.PostModule.DTO;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
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

namespace FPTBlog.Src.PostModule {
    [Route("/api/post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostApiController : Controller {
        private readonly IUploadFileService UploadFileService;
        private readonly IPostService PostService;
        private readonly ICategoryService CategoryService;
        private readonly ITagService TagService;
        public PostApiController(IUploadFileService uploadFileService, IPostService postService, ICategoryService categoryService, ITagService tagService) {
            this.UploadFileService = uploadFileService;
            this.PostService = postService;
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
        public IActionResult AddBlogHandler() {
            Post post = new Post();
            post.Student = (User) this.ViewData["user"];
            post.StudentId = ((User) this.ViewData["user"]).UserId;
            this.PostService.AddPost(post);
            var res = new ServerApiResponse<Post>();
            res.data = post;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("save")]
        public IActionResult SaveBlogHandler([FromBody] SavePostDto input) {
            var res = new ServerApiResponse<Post>();
            ValidationResult result = new SaveBlogDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post blog = this.PostService.GetPostByPostId(input.PostId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            User student = (User) this.ViewData["user"];

            blog.Title = input.Title;
            blog.Content = input.Content;
            blog.Student = student;
            blog.StudentId = student.UserId;

            this.PostService.UpdatePost(blog);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_SAVE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("category")]
        public IActionResult AddCategoryToBlog([FromBody] UpdateCategoryOfPostDto input) {
            var res = new ServerApiResponse<Post>();
            ValidationResult result = new UpdateCategoryOfPostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post blog = this.PostService.GetPostByPostId(input.PostId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            Category category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "categoryId");
                return new NotFoundObjectResult(res.getResponse());
            }

            blog.CategoryId = category.CategoryId;
            blog.Category = category;

            this.PostService.UpdatePost(blog);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("tag")]
        public IActionResult GetTagsByBlogId(string blogId) {
            var res = new ServerApiResponse<List<Tag>>();

            Post blog = this.PostService.GetPostByPostId(blogId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND);
                return new NotFoundObjectResult(res.getResponse());
            }

            var tags = this.PostService.GetTagsFromPost(blog);
            res.data = tags;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("tag")]
        public IActionResult AddTagToBlog([FromBody] ToggleTagToPostDto input) {
            var res = new ServerApiResponse<List<Tag>>();
            ValidationResult result = new ToggleTagToPostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post blog = this.PostService.GetPostByPostId(input.PostId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            // Find tag by name in db, if not found, then create new
            Tag tag = this.TagService.GetTagByName(input.TagName);
            if (tag == null) {
                tag = new Tag();
                tag.Name = input.TagName;
                tag.Status = TagStatus.ACTIVE;
                this.TagService.AddTag(tag);
            }

            List<Tag> tags = this.PostService.GetTagsFromPost(blog);
            // If the current tags of blog already has this tag name, return
            foreach (var item in tags) {
                if (item.Name.Equals(tag.Name)) {
                    res.data = tags;
                    res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
                    return new ObjectResult(res.getResponse());
                }
            }

            this.PostService.AddTagToPost(blog, tag);
            tags.Add(tag);
            res.data = tags;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("tag")]
        public IActionResult RemoveTagFromBlog([FromBody] ToggleTagToPostDto input) {
            var res = new ServerApiResponse<List<Tag>>();
            ValidationResult result = new ToggleTagToPostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post blog = this.PostService.GetPostByPostId(input.PostId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            Tag tag = this.TagService.GetTagByName(input.TagName);
            if (tag == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "tagName");
                return new NotFoundObjectResult(res.getResponse());
            }

            List<Tag> tags = this.PostService.GetTagsFromPost(blog);
            bool canRemove = false;
            foreach (var item in tags) {
                if (item.Name.Equals(tag.Name)) {
                    canRemove = true;
                }
            }

            if (canRemove) {
                tags.Remove(tag);
                this.PostService.RemoveTagFromPost(blog, tag);
            }

            res.data = tags;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("send")]
        public IActionResult PostBlog([FromBody] SendPostDto input) {
            var res = new ServerApiResponse<Post>();
            ValidationResult result = new SendPostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post blog = this.PostService.GetPostByPostId(input.PostId);
            if (blog == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            blog.Status = PostStatus.WAIT;
            this.PostService.UpdatePost(blog);

            res.data = blog;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_POSTED_SUCCESS);
            return new ObjectResult(res.getResponse());
        }


        // [HttpPost("like")]
        // public IActionResult LikeBlog([FromBody] LikePostDto input) {
        //     var res = new ServerApiResponse<Post>();
        //     ValidationResult result = new LikeBlogDtoValidator().Validate(input);
        //     if (!result.IsValid) {
        //         res.mapDetails(result);
        //         return new BadRequestObjectResult(res.getResponse());
        //     }
        //     Post blog = this.PostService.GetPostByPostId(input.PostId);
        //     if (blog == null) {
        //         res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "blogId");
        //         return new NotFoundObjectResult(res.getResponse());
        //     }
        //     User user = (User) this.ViewData["user"];

        //     this.PostService.LikeBlog(blog, user);
        //     res.data = blog;
        //     res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
        //     return new ObjectResult(res.getResponse());
        // }
    }
}
