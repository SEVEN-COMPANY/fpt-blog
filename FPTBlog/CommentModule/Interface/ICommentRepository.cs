using FPTBlog.CommentModule.Entity;

namespace FPTBlog.CommentModule.Interface
{
    public interface ICommentRepository
    {
        public bool SaveComment(Comment comment);
        public Comment GetCommentByCommentId(string commentId);
        public bool DeleteComment(string commentId);
    }
}