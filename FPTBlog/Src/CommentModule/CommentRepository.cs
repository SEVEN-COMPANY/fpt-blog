using System.Linq;
using System.Collections.Generic;

using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.CommentModule.Entity;
using FPTBlog.Src.UserModule.Entity;

using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

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
                comment.Like -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                this.Db.SaveChanges();
                return;
            }
        }

        public void DislikeComment(Comment comment, User user) {
            LikeComment obj = this.Db.LikeComment.FirstOrDefault(item => item.CommentId == comment.CommentId && item.UserId == user.UserId);
            if (obj == null || (obj != null && obj.Expression == Expression.LIKE)) {
                if (obj != null && obj.Expression == Expression.LIKE) {
                    comment.Like -= 1;
                    this.Db.Comment.Update(comment);
                    this.Db.LikeComment.Remove(obj);
                }
                comment.Dislike += 1;
                this.Db.Comment.Update(comment);
                this.AddLikeOrDislikeComment(comment, user, Expression.DISLIKE);
                return;
            }
            if (obj != null && obj.Expression == Expression.DISLIKE) {
                comment.Dislike -= 1;
                this.Db.Comment.Update(comment);
                this.Db.LikeComment.Remove(obj);
                return;
            }
        }
    }
}
