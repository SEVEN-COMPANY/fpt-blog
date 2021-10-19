using System.Collections.Generic;

using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.CommentModule.Entity;

using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.CommentModule {
    public class CommentService : ICommentService {
        private readonly ICommentRepository CommentRepository;
        public CommentService(ICommentRepository commentRepository) {
            this.CommentRepository = commentRepository;
        }
        # region Add, Update, Remove
        public void AddComment(Comment comment) => this.CommentRepository.Add(comment);
        public void UpdateComment(Comment comment) => this.CommentRepository.Update(comment);
        public void RemoveComment(Comment comment) => this.CommentRepository.RemoveAndItsChildComment(comment);
        # endregion

        # region Like, Dislike
        public void LikeComment(Comment comment, User user) => this.CommentRepository.LikeComment(comment, user);
        public void DislikeComment(Comment comment, User user) => this.CommentRepository.DislikeComment(comment, user);
        # endregion

        public Comment GetCommentByCommentId(string commentId) => this.CommentRepository.GetFirstOrDefault(item => item.CommentId == commentId);
        public List<Comment> GetListOriCommentByPostId(string postId){
            List<Comment> list = (List<Comment>)this.CommentRepository.GetAll(item => item.PostId == postId && item.OriCommentId == null, includeProperties: "User");
            return list;
        }
        public List<Comment> GetListSubComment(Comment comment){
            List<Comment> list = (List<Comment>)this.CommentRepository.GetAll(item => item.OriCommentId == comment.CommentId && item.PostId == comment.PostId);
            return list;
        }
    }
}
