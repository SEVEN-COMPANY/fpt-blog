using FluentValidation.Results;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
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
        public IActionResult AddCommentHandler([FromBody] AddCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new AddCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = new Comment();
            comment.Content = input.Content;
            User currentUser = (User) this.ViewData["user"];
            comment.PostId = input.PostId;
            if (input.OriCommentId != null)
                comment.OriCommentId = input.OriCommentId;
            this.CommentService.AddComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("content")]
        public IActionResult UpdateCommentHandler([FromBody] UpdateCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new UpdateCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = this.CommentService.GetCommentByCommentId(input.CommentId);
            comment.Content = input.Content;
            this.CommentService.UpdateComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpDelete("")]
        public IActionResult RemoveCommentHandler([FromBody] RemoveCommentDto input) {
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

        [HttpPost("all")]
        public IActionResult GetListOriComment([FromBody] GetOriCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new GetOriCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            List<Comment> list = this.CommentService.GetListOriCommentByPostId(input.PostId);
            this.ViewData["list-comment"] = list;

            return View(Routers.GetComment.Page);

        }

    }
}
