using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.PostModule.Entity {
    [Table("tblPost")]
    public class Post {
        [Key]
        [Required]
        [StringLength(40)]
        public string PostId {
            get; set;
        }

        [Required]
        [StringLength(250)]
        public string Title {
            get; set;
        }

        [Required]
        public string Content {
            get; set;
        }

        [StringLength(500)]
        public string Note {
            get; set;
        }

        [Required]
        [StringLength(500)]
        public string Description {
            get; set;
        }

        [Required]
        public string CoverUrl {
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
        public PostStatus Status {
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

        public virtual ICollection<PostTag> PostTags {
            get; set;
        }

        public Post() {
            this.PostId = Guid.NewGuid().ToString();
            this.Title = "New Draft";
            this.Content = "<p>Hello there</p>";
            this.Description = "Description";
            this.CoverUrl = "https://picsum.photos/128";
            this.ReadTime = 1;
            this.Like = 0;
            this.Dislike = 0;
            this.View = 0;
            this.Status = PostStatus.DRAFT;
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.Student = null;
            this.Lecturer = null;
            this.Category = null;
            this.Note = "";
            this.PostTags = new List<PostTag>();
        }
    }

    public enum PostStatus {
        DRAFT = 1,
        WAIT = 2,
        APPROVED = 3,
        DENY = 4
    }
}
