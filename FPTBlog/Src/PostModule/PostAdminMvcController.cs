using System;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.PostModule.Interface;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Utils.Common;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.PostModule {
    [Route("/admin/post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostAdminMvcController : Controller {
        private readonly IPostService PostService;
        public PostAdminMvcController(IPostService postService) {
            this.PostService = postService;
        }

        [HttpGet("")]
        public IActionResult GetAllWaitBlogs(string search, PostStatus status, int pageSize = 12, int pageIndex = 0) {

            if (search == null) {
                search = "";
            }

            if ((int) status == 0) {
                var (allPosts, allTotal) = this.PostService.GetAllPosts(pageSize, pageIndex, search);
                this.ViewData["posts"] = allPosts;
                this.ViewData["total"] = allTotal;
            }
            else {
                var (posts, total) = this.PostService.GetPostsByStatus(pageSize, pageIndex, search, status);
                this.ViewData["posts"] = posts;
                this.ViewData["total"] = total;
            }

            var monthlyReport = this.PostService.GetMonthlyReport();


            this.ViewData["postThisMonth"] = monthlyReport.PostThisMonth;
            this.ViewData["postLastMonth"] = monthlyReport.PostLastMonth;
            this.ViewData["viewThisMonth"] = monthlyReport.ViewThisMonth;
            this.ViewData["viewLastMonth"] = monthlyReport.ViewLastMonth;
            this.ViewData["interactThisMonth"] = monthlyReport.InteractThisMonth;
            this.ViewData["interactLastMonth"] = monthlyReport.InteractLastMonth;
            this.ViewData["userThisMonth"] = monthlyReport.UserThisMonth;
            this.ViewData["userLastMonth"] = monthlyReport.UserLastMonth;


            return View(RoutersAdmin.PostGetList.Page);
        }


        [HttpGet("tag")]
        public IActionResult GetBlogsByTagName(int pageSize = 12, int pageIndex = 0, string name = "") {
            var (posts, total) = this.PostService.GetPostsByTagWithCount(pageSize, pageIndex, name);
            this.ViewData["posts"] = posts;
            this.ViewData["total"] = total;

            return View(RoutersAdmin.PostGetPostByTag.Page);
        }


    }
}
