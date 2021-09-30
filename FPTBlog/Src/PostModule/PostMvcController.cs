using System;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.PostModule {
    [Route("post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostMvcController : Controller {
        private readonly IUploadFileService UploadFileService;
        private readonly IPostService PostService;
        private readonly ICategoryService CategoryService;
        public PostMvcController(IUploadFileService uploadFileService, IPostService postService, ICategoryService categoryService) {
            this.UploadFileService = uploadFileService;
            this.PostService = postService;
            this.CategoryService = categoryService;
        }

        [HttpGet("editor")]
        public IActionResult EditorPage(string postId) {

            SelectList list = new SelectList(this.CategoryService.GetCategoryDropList());
            this.ViewData["categories"] = list;
            var post = this.PostService.GetPostByPostId(postId);

            if (post == null) {
                return Redirect(Routers.CommonGetHome.Link);
            }
            this.ViewData["post"] = post;

            return View(Routers.PostGetEditor.Page);
        }

        [HttpGet("preview")]
        public IActionResult PreviewPage(string postId) {
            var post = this.PostService.GetPostByPostId(postId);
            if (post == null) {
                return Redirect(Routers.CommonGetHome.Link);
            }
            Console.WriteLine(post.Category);

            this.ViewData["post"] = post;

            return View(Routers.PostGetPreview.Page);
        }



        [HttpGet("me")]
        public IActionResult GetMyBlogsWithStatus(int pageSize = 12, int pageIndex = 0) {


            var user = (User) this.ViewData["user"];
            var (posts, total) = this.PostService.GetPostsOfStudentWithStatus(pageSize, pageIndex, user.UserId, PostStatus.DRAFT);

            this.ViewData["drafts"] = posts;
            this.ViewData["totalDraft"] = total;

            return View(Routers.PostGetDraftList.Page);
        }

        [HttpGet("")]
        public IActionResult GetBlogByBlogId(string postId) {
            var post = this.PostService.GetPostByPostId(postId);
            this.ViewData["post"] = post;
            return View(Routers.PostGetPost.Page);
        }

        // [HttpGet("")]
        // public IActionResult GetAllBlogs(PostStatus searchStatus, int pageSize = 12, int pageIndex = 0) {
        //     var (posts, total) = this.PostService.GetPostsAndCount(pageSize, pageIndex, searchStatus);
        //     this.ViewData["blogs"] = posts;
        //     this.ViewData["total"] = total;

        //     return Json(new {
        //         blogs = posts,
        //         total = total
        //     });
        // }

        [HttpGet("tag")]
        public IActionResult GetBlogsByTagName(int pageSize = 12, int pageIndex = 0, string name = "") {
            var (posts, total) = this.PostService.GetPostsByTagWithCount(pageSize, pageIndex, name);
            this.ViewData["blogs"] = posts;
            this.ViewData["total"] = total;

            return Json(new {
                posts = posts,
                total = total
            });
        }

        [HttpGet("category")]
        public IActionResult GetBlogsByCategoryName(int pageSize = 12, int pageIndex = 0, string name = "") {
            var (posts, total) = this.PostService.GetPostsByCategoryWithCount(pageSize, pageIndex, name);
            this.ViewData["blogs"] = posts;
            this.ViewData["total"] = total;

            return Json(new {
                posts = posts,
                total = total
            });
        }

        [HttpGet("student")]
        public IActionResult GetBlogsOfStudentWithStatus(PostStatus status, int pageSize = 12, int pageIndex = 0, string studentId = "") {
            var (posts, total) = this.PostService.GetPostsOfStudentWithStatus(pageSize, pageIndex, studentId, status);
            this.ViewData["posts"] = posts;
            this.ViewData["total"] = total;

            return Json(new {
                posts = posts,
                total = total
            });
        }


    }
}
