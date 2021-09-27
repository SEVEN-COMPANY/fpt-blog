using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.BlogModule.Entity {
    [Table("tblBlog")]
    public class Blog {
        [Key]
        [Required]
        [StringLength(40)]
        public string BlogId {
            get; set;
        }

        [Required]
        [StringLength(40)]
        public string Title {
            get; set;
        }

        [Required]
        public string Content {
            get; set;
        }

        [Required]
        [StringLength(500)]
        public string Description {
            get; set;
        }

        [Required]
        [Range(1, 60)]
        public int ReadTime {
            get; set;
        }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Like {
            get; set;
        }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Dislike {
            get; set;
        }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int View {
            get; set;
        }

        [Required]
        public BlogStatus Status {
            get; set;
        }

        [Required]
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }

        [ForeignKey("tblUser")]
        public string StudentId {
            get; set;
        }

        public virtual User Student {
            get; set;
        }

        [ForeignKey("tblUser")]
        public string LecturerId {
            get; set;
        }

        public virtual User Lecturer {
            get; set;
        }

        [ForeignKey("tblCategory")]
        public string CategoryId {
            get; set;
        }

        public virtual Category Category {
            get; set;
        }

        public virtual ICollection<BlogTag> BlogTags {
            get; set;
        }

        public Blog() {
            this.BlogId = Guid.NewGuid().ToString();
            this.Title = "Title";
            this.Content = "<p>Content</p>";
            this.Description = "Description";
            this.ReadTime = 1;
            this.Like = 0;
            this.Dislike = 0;
            this.View = 0;
            this.Status = BlogStatus.DRAFT;
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.Student = null;
            this.Lecturer = null;
            this.Category = null;
            this.BlogTags = new List<BlogTag>();
        }
    }

    public enum BlogStatus {
        DRAFT = 0,
        WAIT = 1,
        APPROVED = 2,
        DENY = 3
    }
}
