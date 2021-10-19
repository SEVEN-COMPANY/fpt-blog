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
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;

namespace FPTBlog.Src.PostModule {
    [Route("post")]
    [ServiceFilter(typeof(UserFilter))]

    public class PostMvcController : Controller {
        private readonly IUploadFileService UploadFileService;
        private readonly IPostService PostService;
        private readonly ICategoryService CategoryService;
        private const string ViewSession = "ViewSession";
        public PostMvcController(IUploadFileService uploadFileService, IPostService postService, ICategoryService categoryService) {
            this.UploadFileService = uploadFileService;
            this.PostService = postService;
            this.CategoryService = categoryService;
        }

        [HttpGet("editor")]
        [ServiceFilter(typeof(AuthGuard))]
        public IActionResult EditorPage(string postId) {
            User user = (User) this.ViewData["user"];

            SelectList list = new SelectList(this.CategoryService.GetCategoryDropList());
            this.ViewData["categories"] = list;
            var post = this.PostService.GetPostByPostId(postId);

            if (post == null) {
                return Redirect(Routers.CommonGetHome.Link);
            }

            if (post.StudentId != user.UserId) {
                return Redirect(Routers.CommonGetHome.Link);
            }

            this.ViewData["post"] = post;

            return View(Routers.PostGetEditor.Page);
        }

        [HttpGet("preview")]
        [ServiceFilter(typeof(AuthGuard))]
        public IActionResult PreviewPage(string postId) {
            var post = this.PostService.GetViewPostByPostId(postId);
            if (post == null) {
                return Redirect(Routers.CommonGetHome.Link);
            }
            this.ViewData["post"] = post;

            return View(Routers.PostGetPreview.Page);
        }

        [HttpGet("me")]
        [ServiceFilter(typeof(AuthGuard))]
        public IActionResult GetMyBlogsWithStatus(int pageSize = 12, int pageIndex = 0) {


            var user = (User) this.ViewData["user"];
            var (posts, total) = this.PostService.GetPostsOfStudentWithStatusForPage(pageSize, pageIndex, user.UserId);

            this.ViewData["drafts"] = posts;
            this.ViewData["totalDraft"] = total;

            return View(Routers.PostGetDraftList.Page);
        }

        [HttpGet("search")]
        public IActionResult GetAllBlogs(string search, string categoryId, int pageSize = 12, int pageIndex = 0) {
            if (search == null) {
                search = "";
            }
            if (categoryId == null) {
                categoryId = "";
            }

            var categoryDropList = this.CategoryService.GetCategoryDropList();
            categoryDropList.Add(new SelectListItem() { Value = "", Text = "All" });
            this.ViewData["categories"] = new SelectList(categoryDropList);

            var (posts, total) = this.PostService.GetPostsAndCount(pageIndex, pageSize, search, categoryId);
            List<PostViewModel> listBlogs = new List<PostViewModel>();
            foreach (var item in posts) {
                PostViewModel pvm = new PostViewModel() {
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                };

                pvm.Post = item;
                listBlogs.Add(pvm);
            }


            this.ViewData["posts"] = listBlogs;
            this.ViewData["total"] = total;

            return View(Routers.PostGetSearch.Page);
        }

        [HttpGet("")]
        public IActionResult GetBlogByBlogId(string postId) {
            var post = this.PostService.GetViewPostByPostId(postId);
            string now = DateTime.Now.ToLongTimeString();
            DateTime timeNow = DateTime.Parse(now);
            string time = this.HttpContext.Session.GetString(ViewSession);
            Dictionary<string, DateTime> list = this.PostService.ConvertStringToViewSession(time);

            bool isRead = false;

            if (list == null) {
                list = new Dictionary<string, DateTime>();
            }
            foreach (var item in list) {
                if (item.Key == postId) {
                    if (item.Value.AddMinutes(post.Post.ReadTime) < DateTime.Now) {
                        post.Post.View += 1;
                    }
                    isRead = true;
                    list.Remove(postId);
                    break;
                }
            }
            if (!isRead) {
                post.Post.View += 1;
            }
            this.PostService.UpdatePost(post.Post);
            list.Add(postId, timeNow);

            this.HttpContext.Session.SetString(ViewSession, this.PostService.ConvertViewSessionToString(list));
            var (suggestion, _) = this.PostService.GetPostsAndCount(0, 3, "", "");
            List<PostViewModel> listSuggestion = new List<PostViewModel>();
            foreach (var item in suggestion) {
                PostViewModel pvm = new PostViewModel() {
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                };

                pvm.Post = item;
                listSuggestion.Add(pvm);
            }
            this.ViewData["suggestion"] = listSuggestion;
            this.ViewData["post"] = post;
            return View(Routers.PostGetPost.Page);
        }
    }
}
