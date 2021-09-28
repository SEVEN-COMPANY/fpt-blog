using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.BlogModule.Entity {
    [Table("tblLikeBlog")]
    public class LikeBlog {
        [Key]
        public string BlogId {
            get; set;
        }
        public virtual Blog Blog {
            get; set;
        }

        [Key]
        public string UserId {
            get; set;
        }

        public virtual User User {
            get; set;
        }
    }
}