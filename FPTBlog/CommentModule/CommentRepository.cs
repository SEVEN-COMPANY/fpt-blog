using FPTBlog.CommentModule.Entity;
using FPTBlog.CommentModule.Interface;
using FPTBlog.Utils;
using System.Linq;

namespace FPTBlog.CommentModule
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DB DB;
        public CommentRepository(DB dB)
        {
            this.DB = dB;
        }

        public bool SaveComment(Comment comment)
        {
            this.DB.Comment.Add(comment);
            return this.DB.SaveChanges() > 0;
        }

        public Comment GetCommentByCommentId(string commentId)
        {
            var comment = this.DB.Comment.FirstOrDefault(item => item.CommentId == commentId);
            return comment;
        }

        public bool DeleteComment(string commentId)
        {
            var comment = this.GetCommentByCommentId(commentId);
            this.DB.Comment.Remove(comment);
            return this.DB.SaveChanges() > 0;
        }
    }
}