using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.Interface;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Utils.Common;

namespace FPTBlog.Src.BlogModule {
    [Route("/admin/post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostAdminMvcController : Controller {
        private readonly IBlogService BlogService;
        public PostAdminMvcController(IBlogService blogService) {
            this.BlogService = blogService;
        }

        [HttpGet("wait")]
        public IActionResult GetAllWaitBlogs() {
            var blogs = this.BlogService.GetAllWaitBlogs();
            this.ViewData["blogs"] = blogs;
            return View(Routers.GetAllBlogs.Page);
        }
    }
}
