using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.CommentModule.Entity;
using System.Collections.Generic;

namespace FPTBlog.Src.CommentModule {
    public class CommentService : ICommentService {
        private readonly ICommentRepository CommentRepository;
        public CommentService(ICommentRepository commentRepository) {
            this.CommentRepository = commentRepository;
        }

        public void AddComment(Comment comment) => this.CommentRepository.Add(comment);

        public Comment GetCommentByCommentId(string commentId) => this.CommentRepository.GetFirstOrDefault(item => item.CommentId == commentId);

        public void UpdateComment(Comment comment) => this.CommentRepository.Update(comment);

        public void RemoveComment(Comment comment) => this.CommentRepository.Remove(comment);

        public List<Comment> GetListSubCommentByOriCommentId(string oriCommentId) => (List<Comment>) this.CommentRepository.GetAll(item => item.OriCommentId == oriCommentId);

        public List<Comment> GetListOriCommentByPostId(string postId) => this.CommentRepository.GetListOriCommentByPostId(postId);

    }
}
