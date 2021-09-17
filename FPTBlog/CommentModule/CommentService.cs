using FPTBlog.CommentModule.Entity;
using FPTBlog.CommentModule.Interface;
using FPTBlog.Utils;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FPTBlog.CommentModule
{
    public class CommentService : ICommentService
    {
        private readonly DB DB;
        private readonly ICommentRepository CommentRepository;

        public CommentService(DB dB, ICommentRepository commentRepository)
        {
            this.DB = dB;
            this.CommentRepository = commentRepository;
        }

        public bool SaveComment(Comment comment)
        {
            return this.CommentRepository.SaveComment(comment);
        }

        public Comment GetCommentByCommentId(string commentId)
        {
            return this.CommentRepository.GetCommentByCommentId(commentId);
        }

        public bool DeleteComment(string commentId)
        {
            return this.CommentRepository.DeleteComment(commentId);
        }
    }
}