using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.TagModule.Entity;

namespace FPTBlog.Src.PostModule.Entity {
    [Table("tblPostTag")]
    public class PostTag {
        [Key]
        public string PostId {
            get; set;
        }

        public virtual Post post {
            get; set;
        }

        [Key]
        public string TagId {
            get; set;
        }

        public virtual Tag Tag {
            get; set;
        }
    }
}
