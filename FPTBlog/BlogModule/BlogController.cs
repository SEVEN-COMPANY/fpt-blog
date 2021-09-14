using System;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.BlogModule
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [HttpGet("")]
        public IActionResult EditorPage()
        {
            return View(Routers.EditorPage.Page);
        }

        [HttpPost("add")]
        public IActionResult AddBlogHandler(string editor)
        {
            Console.WriteLine(editor);
            return View(Routers.AddBlog.Page);
        }
    }
}