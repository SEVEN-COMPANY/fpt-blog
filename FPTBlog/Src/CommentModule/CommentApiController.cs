using FluentValidation.Results;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.CommentModule.DTO;

namespace FPTBlog.Src.CommentModule.Interface {
    [ApiController]
    [Route("/api/comment")]
    public class CommentApiController : Controller {
        private readonly ICommentService CommentService;
        public CommentApiController(ICommentService commentService) {
            this.CommentService = commentService;
        }

        [HttpPost("add")]
        public ObjectResult AddCommentHandler([FromBody] AddCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new AddCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = new Comment();
            comment.Content = input.Content;
            User currentUser = (User) this.ViewData["user"];
            comment.BlogId = input.BlogId;
            this.CommentService.SaveComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

    }
}
