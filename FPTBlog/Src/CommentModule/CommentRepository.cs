using FPTBlog.Src.CommentModule.Interface;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Utils;
using FPTBlog.Src.CommentModule.Entity;

namespace FPTBlog.Src.CommentModule {
    public class CommentRepository : ICommentRepository {
        private readonly DB Db;
        public CommentRepository(DB Db) {
            this.Db = Db;
        }

        public bool SaveComment(Comment comment) {
            this.Db.Comment.Add(comment);
            return this.Db.SaveChanges() > 0;
        }
    }
}
