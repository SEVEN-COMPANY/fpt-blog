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
using FPTBlog.Src.UserModule.Interface;

namespace FPTBlog.Src.PostModule {
    [Route("/api/post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostApiController : Controller {
        private readonly IUploadFileService UploadFileService;
        private readonly IPostService PostService;
        private readonly ICategoryService CategoryService;
        private readonly ITagService TagService;
        private readonly IUserService UserService;
        public PostApiController(IUploadFileService uploadFileService, IPostService postService, ICategoryService categoryService, ITagService tagService, IUserService userService) {
            this.UploadFileService = uploadFileService;
            this.PostService = postService;
            this.CategoryService = categoryService;
            this.TagService = tagService;
            this.UserService = userService;
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
            var res = new ServerApiResponse<Post>();

            User user = (User) this.ViewData["user"];
            var (posts, count) = this.PostService.GetPostsOfStudentWithStatus(user.UserId, PostStatus.DRAFT);
            if (count >= 12) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            var (categories, _) = this.CategoryService.GetCategories();
            Post post = new Post();
            post.Student = user;
            post.StudentId = user.UserId;
            post.CategoryId = categories[0].CategoryId;
            post.Category = categories[0];
            this.PostService.AddPost(post);

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

            Post post = this.PostService.GetPostByPostId(input.PostId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            if (post.Status == PostStatus.APPROVED) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            User student = (User) this.ViewData["user"];

            post.Title = input.Title;
            post.Content = input.Content;
            post.CoverUrl = input.CoverUrl;
            post.ReadTime = input.ReadTime;
            post.Description = input.Description;
            post.Student = student;
            post.StudentId = student.UserId;
            post.Status = PostStatus.DRAFT;

            this.PostService.UpdatePost(post);

            res.data = post;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_SAVE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("category")]
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
        public IActionResult GetTagsByPostId(string postId) {
            var res = new ServerApiResponse<List<Tag>>();

            Post post = this.PostService.GetPostByPostId(postId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND);
                return new NotFoundObjectResult(res.getResponse());
            }

            var tags = this.PostService.GetTagsFromPost(post);
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


        [HttpPost("like")]
        public IActionResult LikePost([FromBody] LikePostDto input) {
            var res = new ServerApiResponse<Post>();
            ValidationResult result = new LikepostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post post = this.PostService.GetPostByPostId(input.PostId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }
            User user = (User) this.ViewData["user"];

            this.PostService.LikePost(post, user);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("dislike")]
        public IActionResult DislikePost([FromBody] LikePostDto input) {
            var res = new ServerApiResponse<Post>();
            ValidationResult result = new LikepostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post post = this.PostService.GetPostByPostId(input.PostId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }
            User user = (User) this.ViewData["user"];

            this.PostService.DislikePost(post, user);
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("suggest")]
        public IActionResult SuggestSearch([FromBody] SuggestSearchDto input) {
            var res = new ServerApiResponse<List<String>>();
            ValidationResult result = new SuggestSearchDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }
            List<string> suggest = this.PostService.GetPostSuggestion(input.Search, input.CategoryId);
            res.data = suggest;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("delete")]
        public IActionResult RemovePost([FromBody] RemoveDraftPostDto input) {
            var res = new ServerApiResponse<Post>();
            ValidationResult result = new RemoveDraftPostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post post = this.PostService.GetPostByPostId(input.PostId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            if (post.Status != PostStatus.DRAFT) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            User user = (User) this.ViewData["user"];
            if (post.StudentId != user.UserId) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            this.PostService.RemovePost(post);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }


        // [HttpPost("save")]
        // public IActionResult SavePost(string postId) {
        //     if (postId == null) {
        //         postId = "";
        //     }

        //     IDictionary<string, object> dataRes = new Dictionary<string, object>();
        //     ServerApiResponse<IDictionary<string, object>> res = new ServerApiResponse<IDictionary<string, object>>();
        //     User user = (User) this.ViewData["user"];

        //     Post post = this.PostService.GetPostByPostId(postId);
        //     if (post == null) {
        //         res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND);
        //         return new BadRequestObjectResult(res.getResponse());
        //     }

        //     this.UserService.SavePost(user, post);

        //     dataRes.Add("user", user);
        //     dataRes.Add("post", post);
        //     res.data = dataRes;
        //     return new ObjectResult(res.getResponse());
        // }

        [HttpGet("chart")]
        public ObjectResult PostChart() {
            var res = new ServerApiResponse<dynamic>();
            var postChart = this.PostService.GetPostChart();
            res.data = postChart;
            return new ObjectResult(res.getResponse());
        }
    }
}
