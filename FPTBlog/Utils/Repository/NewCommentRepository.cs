using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPTBlog.Src.CommentModule.Entity;

namespace FPTBlog.Utils.Repository.Interface {
    public class NewCommentRepository : Repository<Comment>, INewCommentRepository {
        private readonly DB _db;
        public NewCommentRepository(DB db) : base(db) {
            _db = db;
            DbSet = _db.Comment;
        }
    }
}
