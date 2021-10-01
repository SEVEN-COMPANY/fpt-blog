using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.TagModule.Entity;

namespace FPTBlog.Src.PostModule.Entity {
    [Table("tblPostTag")]
    public class PostTag {
        [Key]
        public string PostTagId{
            get;set;
        }

        [Required]
        public string PostId {
            get; set;
        }

        [ForeignKey("PostId")]
        public Post Post {
            get; set;
        }

        [Required]
        public string TagId {
            get; set;
        }

        [ForeignKey("TagId")]
        public Tag Tag {
            get; set;
        }
        public PostTag()
        {
            this.PostTagId = Guid.NewGuid().ToString();
        }
    }


}
