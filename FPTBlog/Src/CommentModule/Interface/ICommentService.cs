using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CommentModule.Interface {
    public interface ICommentService {
        # region Add, Update, Remove
        public void AddComment(Comment comment);
        public void UpdateComment(Comment comment);
        public void RemoveComment(Comment comment);
        # endregion

        # region Like, Dislike
        public void LikeComment(Comment comment, User user);
        public void DislikeComment(Comment comment, User user);
        # endregion

        public List<Comment> GetListOriCommentByPostId(string postId);
        public Comment GetCommentByCommentId(string commentId);
        public List<Comment> GetListSubComment(Comment comment);
    }
}
