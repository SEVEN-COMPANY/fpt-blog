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

        [HttpPost("")]
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
            this.CommentService.AddComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("content")]
        public ObjectResult UpdateCommentHandler([FromBody] UpdateCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new UpdateCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = this.CommentService.GetCommentByCommentId(input.CommentId);
            comment.Content = input.Content;
            this.CommentService.UpdateComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpDelete("")]
        public ObjectResult RemoveCommentHandler([FromBody] RemoveCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new RemoveCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = this.CommentService.GetCommentByCommentId(input.CommentId);
            this.CommentService.RemoveComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

    }
}
