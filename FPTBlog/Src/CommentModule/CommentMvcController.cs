using Microsoft.AspNetCore.Mvc;

using FPTBlog.Utils.Common;
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

        [HttpGet("update")]
        public IActionResult UpdateCommentPage() {
            return View(Routers.UpdateComment.Page);
        }

        [HttpDelete("remove")]
        public IActionResult RemoveCommentPage() {
            return View(Routers.CommonGetHome.Page);
        }
    }
}
