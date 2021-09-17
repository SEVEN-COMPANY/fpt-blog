using FPTBlog.CommentModule.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FPTBlog.CommentModule.Interface
{
    public interface ICommentService
    {
        public bool SaveComment(Comment comment);
        public Comment GetCommentByCommentId(string commentId);
        public bool DeleteComment(string commentId);
    }
}