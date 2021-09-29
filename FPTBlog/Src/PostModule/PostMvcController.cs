using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.BlogModule {
    [Route("post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostMvcController : Controller {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        private readonly ICategoryService CategoryService;
        public PostMvcController(IUploadFileService uploadFileService, IBlogService blogService, ICategoryService categoryService) {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
            this.CategoryService = categoryService;
        }

        [HttpGet("editor")]
        public IActionResult EditorPage(string blogId) {
            SelectList list = new SelectList(this.CategoryService.GetCategoryDropList());
            this.ViewData["categories"] = list;
            var blog = this.BlogService.GetBlogByBlogId(blogId);
            if (blog == null) {
                return Redirect(Routers.Home.Link);
            }
            this.ViewData["blog"] = blog;

            return View(Routers.GetBlogEditor.Page);
        }

        [HttpGet("preview")]
        public IActionResult PreviewPage(string blogId) {
            var blog = this.BlogService.GetBlogByBlogId(blogId);
            if (blog == null) {
                return Redirect(Routers.Home.Link);
            }

            this.ViewData["blog"] = blog;

            return View(Routers.GetBlogPreview.Page);
        }



        [HttpGet("me")]
        public IActionResult GetMyBlogsWithStatus(int pageSize = 12, int pageIndex = 0) {


            var user = (User) this.ViewData["user"];
            var (blogs, total) = this.BlogService.GetBlogsOfStudentWithStatus(pageSize, pageIndex, user.UserId, PostStatus.DRAFT);
            this.ViewData["drafts"] = blogs;
            this.ViewData["totalDraft"] = total;

            return View(Routers.GetMyDraft.Page);
        }

        [HttpGet("")]
        public IActionResult GetAllBlogs(int pageSize = 12, int pageIndex = 0) {
            var (blogs, total) = this.BlogService.GetAllBlogsAndCount(pageSize, pageIndex);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;

            return Json(new {
                blogs = blogs,
                total = total
            });
        }

        [HttpGet("tag")]
        public IActionResult GetBlogsByTagName(int pageSize = 12, int pageIndex = 0, string name = "") {
            var (blogs, total) = this.BlogService.GetBlogsByTagAndCount(pageSize, pageIndex, name);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;

            return Json(new {
                blogs = blogs,
                total = total
            });
        }

        [HttpGet("category")]
        public IActionResult GetBlogsByCategoryName(int pageSize = 12, int pageIndex = 0, string name = "") {
            var (blogs, total) = this.BlogService.GetBlogsByCategoryAndCount(pageSize, pageIndex, name);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;

            return Json(new {
                blogs = blogs,
                total = total
            });
        }

        [HttpGet("student")]
        public IActionResult GetBlogsOfStudentWithStatus(PostStatus status, int pageSize = 12, int pageIndex = 0, string studentId = "") {
            var (blogs, total) = this.BlogService.GetBlogsOfStudentWithStatus(pageSize, pageIndex, studentId, status);
            this.ViewData["blogs"] = blogs;
            this.ViewData["total"] = total;

            return Json(new {
                blogs = blogs,
                total = total
            });
        }

        [HttpGet("blogId")]
        public IActionResult GetBlogByBlogId(string blogId) {
            var blog = this.BlogService.GetBlogByBlogId(blogId);
            this.ViewData["blog"] = blog;
            return View(Routers.GetBlog.Page);
        }
    }
}
