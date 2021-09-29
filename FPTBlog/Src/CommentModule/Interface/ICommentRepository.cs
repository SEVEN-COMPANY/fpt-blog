using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.CommentModule.Entity;
using System.Threading.Tasks;

namespace FPTBlog.Src.CommentModule.Interface {
    public interface ICommentRepository {
        public bool SaveComment(Comment comment);
    }
}
