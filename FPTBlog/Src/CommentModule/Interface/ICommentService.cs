using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CommentModule.Interface {
    public interface ICommentService {
        #region Manage comment
        public void AddComment(Comment comment);
        public void UpdateComment(Comment comment);
        public void RemoveComment(Comment comment);
        #endregion

        #region Get single or list comment
        public Comment GetCommentByCommentId(string commentId);
        public List<Comment> GetListOriCommentByPostId(string postId);
        public List<Comment> GetListSubComment(Comment comment);
        #endregion

        #region Like and dislike comment
        public void LikeComment(Comment comment, User user);
        public void DislikeComment(Comment comment, User user);
        #endregion
    }
}
