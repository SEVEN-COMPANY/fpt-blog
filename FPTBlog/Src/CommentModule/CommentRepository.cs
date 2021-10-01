using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.CommentModule {
    public class CommentRepository : Repository<Comment>, ICommentRepository {
        private readonly DB Db;
        public CommentRepository(DB Db) : base(Db) {
            this.Db = Db;
        }

        public List<Comment> GetListOriCommentByPostId(string postId) {
            List<Comment> comment = (from Comment in this.Db.Comment
                                     where Comment.PostId.Equals(postId) && Comment.OriCommentId != null
                                     select Comment).ToList();
            return comment;
        }
    }
}