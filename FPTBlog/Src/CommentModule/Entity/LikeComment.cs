using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.CommentModule.Entity
{
    public enum Expression {
        LIKE = 1,
        DISLIKE = 2
    }
    [Table("tblLikeComment")]
    public class LikeComment
    {
        [Key]
        [Required]
        public string LikeCommentId {
            get; set;
        }

        [Required]
        public string CommentId {
            get; set;
        }

        [ForeignKey("CommentId")]
        public virtual Comment Comment {
            get; set;
        }

        [Required]
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }

        [Required]
        public string UserId {
            get; set;
        }

        [ForeignKey("UserId")]
        public virtual User User {
            get; set;
        }

        [Required]
<<<<<<< HEAD
        public Expression expression {
=======
        public Expression Expression {
>>>>>>> 1c795350fc512b63ecad4e0691086cbdd906aff9
            get; set;
        }

        public LikeComment() {
<<<<<<< HEAD
=======
            this.LikeCommentId = Guid.NewGuid().ToString();
>>>>>>> 1c795350fc512b63ecad4e0691086cbdd906aff9
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }
}
