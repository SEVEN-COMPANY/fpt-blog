using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.CommentModule.Entity;
using System.Collections.Generic;

namespace FPTBlog.Src.CommentModule {
    public class CommentService : ICommentService {
        private readonly ICommentRepository CommentRepository;
        public CommentService(ICommentRepository commentRepository) {
            this.CommentRepository = commentRepository;
        }

        public bool SaveComment(Comment comment) {
            return this.CommentRepository.SaveComment(comment);
        }

    }
}
