using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.CommentModule.Entity;
using System.Threading.Tasks;

namespace FPTBlog.Src.CommentModule.Interface {
    public interface ICommentRepository {
        public void AddComment(Comment comment);
        public Comment GetCommentByCommentId(string commentId);
        public void UpdateComment(Comment comment);
        public void RemoveComment(Comment comment);
    }
}
