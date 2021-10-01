using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.PostModule.Entity {
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
        public string UserId {
            get; set;
        }

        [ForeignKey("UserId")]
        public virtual User User {
            get; set;
        }
    }
}
