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


    }
}
