using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.Interface;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Utils.Common;

namespace FPTBlog.Src.BlogModule {
    [Route("/admin/blog")]
    [ServiceFilter(typeof(AuthGuard))]
    public class BlogAdminMvcController : Controller {
        private readonly IBlogService BlogService;
        public BlogAdminMvcController(IBlogService blogService) {
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