using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.CommentModule.Entity;
using System.Threading.Tasks;
using FPTBlog.Utils.Repository.Interface;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.CommentModule.Interface {
    public interface ICommentRepository : IRepository<Comment> {
        public void RemoveAndItsChildComment(Comment comment);
        public void LikeComment(Comment comment, User user);
        public void DislikeComment(Comment comment, User user);
    }
}
