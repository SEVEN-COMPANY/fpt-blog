using System;
using FluentValidation.Results;
using FPTBlog.AuthModule;
using FPTBlog.BlogModule.Interface;
using FPTBlog.CommentModule.DTO;
using FPTBlog.CommentModule.Entity;
using FPTBlog.CommentModule.Interface;
using FPTBlog.UserModule.Entity;
using FPTBlog.Utils;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.CommentModule
{
    [Route("comment")]
    [ServiceFilter(typeof(AuthGuard))]
    public class CommentController : Controller
    {
        private readonly ICommentService CommentService;
        private readonly IBlogService blogService;
        private readonly DB DB;

        public CommentController(DB dB, ICommentService commentService, IBlogService blogService)
        {
            this.DB = dB;
            this.CommentService = commentService;
            this.blogService = blogService;
        }

        [HttpPost("add")]
        public IActionResult AddComment(string content, string blogId)
        {
            User user = (User)this.ViewData["user"];
            var input = new AddCommentDto()
            {
                UserId = user.UserId,
                Content = content,
                BlogId = blogId
            };
            var result = new AddCommentDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.GetBlog.Page);
            }

            if (user == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL, ViewData);
                return Redirect(Routers.Login.Link);
            }
            var isExistBlog = this.blogService.GetBlogByBlogId(blogId);
            if (isExistBlog == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, ViewData);
                return Redirect(Routers.GetBlog.Page);
            }

            var comment = new Comment();
            comment.CommentId = Guid.NewGuid().ToString();
            comment.UserId = input.UserId;
            comment.Content = input.Content;
            comment.BlogId = input.BlogId;
            comment.CreateDate = DateTime.Now.ToLongDateString();
            comment.Like = 0;
            comment.Dislike = 0;
            comment.SubCommentId = "";

            this.CommentService.SaveComment(comment);
            return Redirect(Routers.GetBlog.Page);
        }

        [HttpPost("delete")]
        public IActionResult DeleteComment(string commentId)
        {
            var isExistComment = this.CommentService.GetCommentByCommentId(commentId);
            if (isExistComment == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, ViewData);
                return Redirect(Routers.GetBlog.Page);
            }

            this.CommentService.DeleteComment(commentId);
            return Redirect(Routers.GetBlog.Page);

        }
    }
}