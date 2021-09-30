using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.CommentModule.Entity;
using System.Collections.Generic;

namespace FPTBlog.Src.CommentModule {
    public class CommentService : ICommentService {
        private readonly ICommentRepository CommentRepository;
        public CommentService(ICommentRepository commentRepository) {
            this.CommentRepository = commentRepository;
        }

        public void AddComment(Comment comment) => this.CommentRepository.AddComment(comment);

        public Comment GetCommentByCommentId(string commentId) {
            return this.CommentRepository.GetCommentByCommentId(commentId);
        }

        public void UpdateComment(Comment comment) => this.CommentRepository.UpdateComment(comment);

        public void RemoveComment(Comment comment) => this.CommentRepository.RemoveComment(comment);


    }
}
