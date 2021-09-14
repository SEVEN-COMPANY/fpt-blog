using System;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("image")]
        public string UploadImageHandler(IFormFile input)
        {
            Console.WriteLine(input);
            return "/hello.jpg";
        }

        [HttpPost("save")]
        public string HandleOnSave(string title, string contain)
        {
            Console.WriteLine(title);
            Console.WriteLine(contain);
            // Console.WriteLine(editor);
            return "ok";
        }


        [HttpPost("")]
        public IActionResult AddBlogHandler(string title, string contain)
        {
            Console.WriteLine(title);
            Console.WriteLine(contain);
            // Console.WriteLine(editor);
            return View(Routers.AddBlog.Page);
        }
    }
}