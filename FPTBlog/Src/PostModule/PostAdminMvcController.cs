using FPTBlog.Src.AuthModule;
using FPTBlog.Src.PostModule.Interface;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllWaitBlogs() {

            return View(RoutersAdmin.PostGetList.Page);
        }
        [HttpGet("wait")]
        // public IActionResult GetAllWaitBlogs() {
        //     var (posts, count) = this.PostService.GetWaitPostsWithCount();
        //     this.ViewData["posts"] = posts;
        //     this.ViewData["count"] = count;
        //     return View("");
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
    }
}
