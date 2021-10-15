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
        public Expression expression {
            get; set;
        }

        public LikeComment() {
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }
}
