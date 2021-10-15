using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.TagModule.Entity {
    [Table("tblTag")]
    public class Tag {
        [Key]
        [Required]
        [StringLength(40)]
        public string TagId {
            get; set;
        }

        [Required]

        public string Name {
            get; set;
        }

        [Required]
        public TagStatus Status {
            get; set;
        }

        [Required]
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }

        public virtual ICollection<PostTag> PostTags {
            get; set;
        }

        public Tag() {
            this.TagId = Guid.NewGuid().ToString();
            this.Name = "";
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.Status = TagStatus.ACTIVE;
            this.PostTags = new List<PostTag>();
        }

        public override string ToString() {
            return "Tag: \n TagId: " + TagId + " \nName: " + Name
            + " \nCreateDate: " + CreateDate;
        }
    }

    public enum TagStatus {
        ACTIVE = 1,
        INACTIVE = 2
    }
}
