using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.CommentModule.Interface;

namespace FPTBlog.Src.CommentModule {
    [Route("user/comment")]
    public class CommentMvcController : Controller {
        private readonly ICommentService CommentService;
        public CommentMvcController(ICommentService commentService) {
            this.CommentService = commentService;
        }

        [HttpGet("add")]
        public IActionResult AddCommentPage() {
            return View(Routers.AddComment.Page);
        }
    }
}
