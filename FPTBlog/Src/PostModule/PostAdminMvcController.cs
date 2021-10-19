using System.Collections.Generic;

using FPTBlog.Src.AuthModule;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using FPTBlog.Utils.Common;

namespace FPTBlog.Src.PostModule {
    [Route("/admin/post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostAdminMvcController : Controller {
        private readonly IPostService PostService;
        public PostAdminMvcController(IPostService postService) {
            this.PostService = postService;
        }

        [HttpGet("")]
        public IActionResult GetAllWaitBlogs(string searchName, PostStatus searchStatus, int pageSize = 12, int pageIndex = 0) {



            if (searchName == null) {
                searchName = "";
            }
            var approvedStatus = new List<SelectListItem>(){
                new SelectListItem(){ Value = PostStatus.APPROVED.ToString(), Text = "Approve"},
                new SelectListItem(){  Value =  PostStatus.DENY.ToString(), Text = "Deny"},
            };

            this.ViewData["approvedStatus"] = new SelectList(approvedStatus);

            var statusList = this.PostService.GetPostStatusDropList();
            statusList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList list = new SelectList(statusList, "");
            this.ViewData["statusSearch"] = list;

            if ((int) searchStatus == 0) {
                var (allPosts, allTotal) = this.PostService.GetAllPosts(pageSize, pageIndex, searchName);
                this.ViewData["posts"] = allPosts;
                this.ViewData["total"] = allTotal;
            }
            else {
                var (posts, total) = this.PostService.GetPostsByStatus(pageSize, pageIndex, searchName, searchStatus);
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
