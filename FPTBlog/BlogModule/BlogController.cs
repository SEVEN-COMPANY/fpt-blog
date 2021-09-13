using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.BlogModule
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [HttpGet("")]
        public IActionResult CreateBlogPage()
        {
            return View(Routers.CreateBlog.Page);
        }
    }
}