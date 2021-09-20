using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.BlogModule.DTO;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.Src.BlogModule
{
    [Route("blog")]
    [ServiceFilter(typeof(AuthGuard))]
    public class BlogMvcController : Controller
    {
        private readonly IUploadFileService UploadFileService;
        private readonly IBlogService BlogService;
        public BlogMvcController(IUploadFileService uploadFileService, IBlogService blogService)
        {
            this.UploadFileService = uploadFileService;
            this.BlogService = blogService;
        }

        [HttpGet("")]
        public IActionResult EditorPage()
        {

            return View(Routers.EditorPage.Page);
        }

    }
}