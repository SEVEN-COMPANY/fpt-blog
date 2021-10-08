using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.PostModule.Entity {
    public enum Expression {
        LIKE = 1,
        DISLIKE = 2
    }

    [Table("tblLikePost")]
    public class LikePost {
        [Key]
        [Required]
        public string LikePostId {
            get; set;
        }

        [Required]
        public string PostId {
            get; set;
        }

        [ForeignKey("PostId")]
        public virtual Post Post {
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

        public LikePost() {
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }
}
