using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;

namespace FPTBlog.Src.CommentModule {
    public class CommentRepository : ICommentRepository {
        private readonly DB Db;
        public CommentRepository(DB Db) {
            this.Db = Db;
        }

        public void AddComment(Comment comment) => this.Db.Comment.Add(comment);

        public Comment GetCommentByCommentId(string commentId) {
            Comment comment = this.Db.Comment.FirstOrDefault(item => item.CommentId == commentId);
            return comment;
        }

        public void UpdateComment(Comment comment) => this.Db.Comment.Update(comment);

        public void RemoveComment(Comment comment) => this.Db.Comment.Remove(comment);
    }
}
