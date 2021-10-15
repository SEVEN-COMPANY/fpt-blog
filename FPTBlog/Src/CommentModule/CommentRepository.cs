using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Utils.Repository;
<<<<<<< HEAD
=======
using System;
>>>>>>> 1c795350fc512b63ecad4e0691086cbdd906aff9
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.CommentModule {
    public class CommentRepository : Repository<Comment>, ICommentRepository {
        private readonly DB Db;
        public CommentRepository(DB Db) : base(Db) {
            this.Db = Db;
        }

        public void RemoveAndItsChildComment(Comment comment) {
            List<Comment> comments = this.Db.Comment.Where(item => item.OriCommentId == comment.CommentId).ToList();
            if (comments.Count > 0) {
                this.Remove(comments);
            }
            this.Remove(comment);
            this.Db.SaveChanges();
        }

<<<<<<< HEAD
        public void LikeComment(Comment comment, User user) {
            LikeComment obj = this.Db.LikeComment.FirstOrDefault(item => item.CommentId == comment.CommentId && item.UserId == user.UserId);
            if (obj == null || (obj != null && (int) obj.expression == 2)) {
                if (obj != null && (int) obj.expression == 2) {
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
                like.expression = Expression.LIKE;
                this.Db.LikeComment.Add(like);
                this.Db.SaveChanges();
                return;
            }
            if (obj != null && (int) obj.expression == 1) {
=======
        private void AddLikeOrDislikeComment(Comment comment, User user, Expression expression) {
            LikeComment likeOrDislike = new LikeComment();
            likeOrDislike.CommentId = comment.CommentId;
            likeOrDislike.Comment = comment;
            likeOrDislike.UserId = user.UserId;
            likeOrDislike.User = user;
            likeOrDislike.Expression = expression;

            this.Db.LikeComment.Add(likeOrDislike);
            this.Db.SaveChanges();
        }

        public void LikeComment(Comment comment, User user) {
            LikeComment obj = this.Db.LikeComment.FirstOrDefault(item => item.CommentId == comment.CommentId && item.UserId == user.UserId);

            if (obj == null) {
                comment.Like += 1;
                this.Db.Comment.Update(comment);
                this.AddLikeOrDislikeComment(comment, user, Expression.LIKE);
                return;
            }

            if (obj != null && obj.Expression == Expression.DISLIKE) {
                comment.Like += 1;
                comment.Dislike -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                this.AddLikeOrDislikeComment(comment, user, Expression.LIKE);
                this.Db.SaveChanges();
                return;
            }

            if (obj != null && obj.Expression == Expression.LIKE) {
>>>>>>> 1c795350fc512b63ecad4e0691086cbdd906aff9
                comment.Like -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }

        public void DislikeComment(Comment comment, User user) {
            LikeComment obj = this.Db.LikeComment.FirstOrDefault(item => item.CommentId == comment.CommentId && item.UserId == user.UserId);
<<<<<<< HEAD
            if (obj == null || (obj != null && (int) obj.expression == 1)) {
                if (obj != null && (int) obj.expression == 1) {
=======
            if (obj == null || (obj != null && obj.Expression == Expression.LIKE)) {
                if (obj != null && obj.Expression == Expression.LIKE) {
>>>>>>> 1c795350fc512b63ecad4e0691086cbdd906aff9
                    comment.Like -= 1;
                    this.Db.Comment.Update(comment);
                    this.Db.LikeComment.Remove(obj);
                }
                comment.Dislike += 1;
                this.Db.Comment.Update(comment);
<<<<<<< HEAD
                LikeComment dislike = new LikeComment();
                dislike.LikeCommentId = Guid.NewGuid().ToString();
                dislike.CommentId = comment.CommentId;
                dislike.Comment = comment;
                dislike.UserId = user.UserId;
                dislike.User = user;
                dislike.CreateDate = DateTime.Now.ToShortDateString();
                dislike.expression = Expression.DISLIKE;
                this.Db.LikeComment.Add(dislike);
                this.Db.SaveChanges();
                return;
            }
            if (obj != null && (int) obj.expression == 2) {
                comment.Dislike -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                this.Db.SaveChanges();
=======
                this.AddLikeOrDislikeComment(comment, user, Expression.DISLIKE);
                return;
            }
            if (obj != null && obj.Expression == Expression.DISLIKE) {
                comment.Dislike -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
>>>>>>> 1c795350fc512b63ecad4e0691086cbdd906aff9
                return;
            }
        }
    }
}
