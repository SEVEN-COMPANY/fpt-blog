using FluentValidation.Results;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using FPTBlog.Src.CommentModule.DTO;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;
using System;
using FPTBlog.Src.AuthModule;

namespace FPTBlog.Src.CommentModule.Interface {
    [ApiController]
    [Route("/api/comment")]
    [ServiceFilter(typeof(AuthGuard))]
    public class CommentApiController : Controller {
        private readonly ICommentService CommentService;
        private readonly IPostService PostService;
        public CommentApiController(ICommentService commentService, IPostService postService) {
            this.CommentService = commentService;
            this.PostService = postService;
        }

        [HttpPost("")]
        public IActionResult AddCommentHandler([FromBody] AddCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new AddCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post post = this.PostService.GetPostByPostId(input.PostId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = new Comment();
            comment.Content = input.Content;
            User user = (User) this.ViewData["user"];

            comment.UserId = user.UserId;
            comment.PostId = input.PostId;

            if (input.OriCommentId != null) {
                Comment oriComment = this.CommentService.GetCommentByCommentId(input.OriCommentId);
                if (oriComment == null) {
                    res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "oriCommentId");
                    return new BadRequestObjectResult(res.getResponse());
                }
                comment.OriCommentId = oriComment.CommentId;
                comment.OriComment = oriComment;
            }
            this.CommentService.AddComment(comment);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        public IActionResult UpdateCommentHandler([FromBody] UpdateCommentDto input) {
            var res = new ServerApiResponse<Comment>();

            ValidationResult result = new UpdateCommentDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Comment comment = this.CommentService.GetCommentByCommentId(input.CommentId);
            if (comment == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "commentId");
                return new BadRequestObjectResult(res.getResponse());
            }

            comment.Content = input.Content;
            comment.CreateDate = DateTime.Now.ToShortDateString();
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
            if (comment == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "commentId");
                return new BadRequestObjectResult(res.getResponse());
            }

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
            this.ViewData["listComment"] = list;

            return Json(new {
                listComment = list
            });

        }

    }
}
