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

        [HttpGet("wait")]
        public IActionResult GetAllWaitBlogs() {
            var (posts, count) = this.PostService.GetWaitPostsWithCount();
            this.ViewData["posts"] = posts;
            this.ViewData["count"] = count;
            return View(Routers.GetAllBlogs.Page);
        }
    }
}
