using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.DTO;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.BlogModule
{
    [Route("blog")]
    [ServiceFilter(typeof(AuthGuard))]
    public class BlogMvcController : Controller
    {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        private readonly ICategoryService CategoryService;
        public BlogMvcController(IUploadFileService uploadFileService, IBlogService blogService, ICategoryService categoryService)
        {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
            this.CategoryService = categoryService;
        }

        [HttpGet("editor")]
        public IActionResult EditorPage()
        {
            SelectList list = new SelectList(this.CategoryService.GetRadioCategoryList());
            this.ViewData["categoryId"] = list;

            return View(Routers.EditorPage.Page);
        }

        [HttpGet("")]
        public IActionResult GetAllBlogs(int pageSize, int pageIndex)
        {
            var (blogs, total) = this.BlogService.GetAllBlogsAndCount(pageSize, pageIndex);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;

            return Json(new{
                blogs = blogs,
                total = total
            });
        }

        [HttpGet("tag")]
        public IActionResult GetBlogByTagName(int pageSize, int pageIndex, string name)
        {
            var (blogs, total) = this.BlogService.GetBlogsByTagAndCount(pageSize, pageIndex, name);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;
            
            return Json(new{
                blogs = blogs,
                total = total
            });
        }

        [HttpGet("category")]
        public IActionResult GetBlogByCategoryName(int pageSize, int pageIndex, string name){
            var (blogs, total) = this.BlogService.GetBlogsByCategoryAndCount(pageSize, pageIndex, name);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;
            
             return Json(new{
                blogs = blogs,
                total = total
            });
        }

    }
}