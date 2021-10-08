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

            Console.WriteLine(status);


            var monthlyReport = this.PostService.GetMonthlyReport();
            var (posts, total) = this.PostService.getPostsByStatus(pageSize, pageIndex, search, status);

            this.ViewData["postThisMonth"] = monthlyReport.PostThisMonth;
            this.ViewData["postLastMonth"] = monthlyReport.PostLastMonth;
            this.ViewData["viewThisMonth"] = monthlyReport.ViewThisMonth;
            this.ViewData["viewLastMonth"] = monthlyReport.ViewLastMonth;
            this.ViewData["interactThisMonth"] = monthlyReport.InteractThisMonth;
            this.ViewData["interactLastMonth"] = monthlyReport.InteractLastMonth;
            this.ViewData["userThisMonth"] = monthlyReport.UserThisMonth;
            this.ViewData["userLastMonth"] = monthlyReport.UserLastMonth;
            this.ViewData["posts"] = posts;
            this.ViewData["total"] = total;
            Console.WriteLine(posts.Count);

            return View(RoutersAdmin.PostGetList.Page);
        }


        [HttpGet("tag")]
        public IActionResult GetBlogsByTagName(int pageSize = 12, int pageIndex = 0, string name = "") {
            var (posts, total) = this.PostService.GetPostsByTagWithCount(pageSize, pageIndex, name);
            this.ViewData["posts"] = posts;
            this.ViewData["total"] = total;

            return View(RoutersAdmin.PostGetPostByTag.Page);
        }

        [HttpGet("report")]
        public IActionResult MonthlyReport(string search, PostStatus status, int pageSize = 12, int pageIndex = 0) {
            if (search == null) {
                search = "";
            }



            var monthlyReport = this.PostService.GetMonthlyReport();
            var (posts, total) = this.PostService.getPostsByStatus(pageSize, pageIndex, search, status);

            this.ViewData["postThisMonth"] = monthlyReport.PostThisMonth;
            this.ViewData["postLastMonth"] = monthlyReport.PostLastMonth;
            this.ViewData["viewThisMonth"] = monthlyReport.ViewThisMonth;
            this.ViewData["viewLastMonth"] = monthlyReport.ViewLastMonth;
            this.ViewData["interactThisMonth"] = monthlyReport.InteractThisMonth;
            this.ViewData["interactLastMonth"] = monthlyReport.InteractLastMonth;
            this.ViewData["userThisMonth"] = monthlyReport.UserThisMonth;
            this.ViewData["userLastMonth"] = monthlyReport.UserLastMonth;
            this.ViewData["posts"] = posts;
            this.ViewData["total"] = total;
            return Json(new {
                postThisMonth = monthlyReport.PostThisMonth,
                postLastMonth = monthlyReport.PostLastMonth,
                viewThisMonth = monthlyReport.ViewThisMonth,
                viewLastMonth = monthlyReport.ViewLastMonth,
                interactThisMonth = monthlyReport.InteractThisMonth,
                interactLastMonth = monthlyReport.InteractLastMonth,
                userThisMonth = monthlyReport.UserThisMonth,
                userLastMonth = monthlyReport.UserLastMonth,
                posts = posts,
                total = total
            });
        }
    }
}
