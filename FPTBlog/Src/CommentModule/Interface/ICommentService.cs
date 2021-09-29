using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CommentModule.Interface {
    public interface ICommentService {
        public bool SaveComment(Comment comment);
    }
}
