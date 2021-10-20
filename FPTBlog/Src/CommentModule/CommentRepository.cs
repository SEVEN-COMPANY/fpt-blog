using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Utils.Repository;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.CommentModule {
    public class CommentRepository : Repository<Comment>, ICommentRepository {
        private readonly DB Db;
        public CommentRepository(DB Db) : base(Db) {
            this.Db = Db;
        }
        #region Remove comment
        public void RemoveAndItsChildComment(Comment comment){
            List<Comment> comments = this.Db.Comment.Where(item => item.OriCommentId == comment.CommentId).ToList();
            if(comments.Count > 0){
                this.Remove(comments);
            }
            this.Remove(comment);
            this.Db.SaveChanges();
        }
        #endregion

        #region Like and dislike comment
        public void LikeComment(Comment comment, User user) {
            LikeComment obj = this.Db.LikeComment.FirstOrDefault(item => item.CommentId == comment.CommentId && item.UserId == user.UserId);
            if (obj == null || (obj != null && (int) obj.Expression == 2)) {
                if (obj != null && (int) obj.Expression == 2) {
                    comment.Dislike -= 1;
                    this.Db.Comment.Update(comment);
                    this.Db.LikeComment.Remove(obj);
                }
                comment.Like += 1;
                this.Db.Comment.Update(comment);
                LikeComment like = new LikeComment();
                like.LikeCommentId = Guid.NewGuid().ToString();
                like.CommentId = comment.CommentId;
                like.Comment = comment;
                like.UserId = user.UserId;
                like.User = user;
                like.CreateDate = DateTime.Now.ToShortDateString();
                like.Expression = Expression.LIKE;
                this.Db.LikeComment.Add(like);
                this.Db.SaveChanges();
                return;
            }
            if (obj != null && (int) obj.Expression == 1) {
                comment.Like -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }
        public void DislikeComment(Comment comment, User user) {
            LikeComment obj = this.Db.LikeComment.FirstOrDefault(item => item.CommentId == comment.CommentId && item.UserId == user.UserId);
            if (obj == null || (obj != null && (int) obj.Expression == 1)) {
                if (obj != null && (int) obj.Expression == 1) {
                    comment.Like -= 1;
                    this.Db.Comment.Update(comment);
                    this.Db.LikeComment.Remove(obj);
                }
                comment.Dislike += 1;
                this.Db.Comment.Update(comment);
                LikeComment dislike = new LikeComment();
                dislike.LikeCommentId = Guid.NewGuid().ToString();
                dislike.CommentId = comment.CommentId;
                dislike.Comment = comment;
                dislike.UserId = user.UserId;
                dislike.User = user;
                dislike.CreateDate = DateTime.Now.ToShortDateString();
                dislike.Expression = Expression.DISLIKE;
                this.Db.LikeComment.Add(dislike);
                this.Db.SaveChanges();
                return;
            }
            if (obj != null && (int) obj.Expression == 2) {
                comment.Dislike -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }
        #endregion
    }
}
