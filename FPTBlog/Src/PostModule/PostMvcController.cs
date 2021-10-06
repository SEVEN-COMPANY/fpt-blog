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
using System.Collections.Generic;

namespace FPTBlog.Src.PostModule {
    [Route("post")]
    [ServiceFilter(typeof(UserFilter))]

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
        [ServiceFilter(typeof(AuthGuard))]
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
            var (posts, total) = this.PostService.GetPostsOfStudentWithStatus(pageSize, pageIndex, user.UserId, PostStatus.DRAFT);

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


            this.ViewData["blogs"] = listBlogs;
            this.ViewData["total"] = total;

            return View(Routers.PostGetSearch.Page);
        }

        [HttpGet("")]
        public IActionResult GetBlogByBlogId(string postId) {
            var post = this.PostService.GetViewPostByPostId(postId);
            this.ViewData["post"] = post;
            return View(Routers.PostGetPost.Page);
        }




        // [HttpGet("category")]
        // public IActionResult GetBlogsByCategoryName(int pageSize = 12, int pageIndex = 0, string name = "") {
        //     var (posts, total) = this.PostService.GetPostsByCategoryWithCount(pageSize, pageIndex, name);
        //     this.ViewData["blogs"] = posts;
        //     this.ViewData["total"] = total;

        //     return Json(new {
        //         posts = posts,
        //         total = total
        //     });
        // }

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

        // [HttpGet("home")]
        // public IActionResult GetPostForHome() {
        //     var (listHighestPoint, _) = this.PostService.GetHighestPointPosts(4);
        //     var (listPopular, _) = this.PostService.GetPopularPosts(16);
        //     var (listNewest, _) = this.PostService.GetNewestPosts(16);

        //     PostViewModel postViewModelTop1 = new PostViewModel() {
        //         Post = listHighestPoint[0],
        //         NumberOfComment = this.PostService.GetCommentOfPost(listHighestPoint[0]).Item2
        //     };

        //     List<PostViewModel> listTop3 = new List<PostViewModel>();
        //     for (int i = 1; i <= 3; i++) {
        //         PostViewModel pvm = new PostViewModel() {
        //             Post = listHighestPoint[i],
        //             NumberOfComment = this.PostService.GetCommentOfPost(listHighestPoint[i]).Item2
        //         };
        //         listTop3.Add(pvm);
        //     }

        //     List<PostViewModel> listMiddle16 = new List<PostViewModel>();
        //     foreach (var item in listPopular) {
        //         listMiddle16.Add(new PostViewModel() {
        //             Post = item,
        //             NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
        //         });
        //     }

        //     List<PostViewModel> listBottom16 = new List<PostViewModel>();
        //     foreach (var item in listNewest) {
        //         listBottom16.Add(new PostViewModel() {
        //             Post = item,
        //             NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
        //         });
        //     }

        //     this.ViewData["top1"] = postViewModelTop1;
        //     this.ViewData["top3"] = listTop3;
        //     this.ViewData["middle16"] = listMiddle16;
        //     this.ViewData["bottom16"] = listBottom16;

        //     return Json(new {
        //         postViewModelTop1 = postViewModelTop1,
        //         listTop3 = listTop3,
        //         listMiddle16 = listMiddle16,
        //         listBottom16 = listBottom16
        //     });
        // }
    }
}
