using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.BlogModule.Entity;

namespace FPTBlog.Src.CommentModule.Entity {
    [Table("tblComment")]
    public class Comment {
        [Key]
        [Required]
        [StringLength(50)]
        public string CommentId {
            get; set;
        }

        [Required]
        public string Content {
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
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }

        [StringLength(50)]
        public string SubcommentId {
            get; set;
        }

        [ForeignKey("tblBlog")]
        [StringLength(40)]
        public string BlogId {
            get; set;
        }
        public virtual Blog Blog {
            get; set;
        }


        [ForeignKey("tblUser")]
        [StringLength(40)]
        public string UserId {
            get; set;
        }
        public virtual User User {
            get; set;
        }


        public Comment() {
            this.CommentId = Guid.NewGuid().ToString();
            this.Content = "<p>Content</p>";
            this.Like = 0;
            this.Dislike = 0;
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.SubcommentId = null;
            this.BlogId = null;
            this.UserId = null;
        }

        public override string ToString() {
            return "Comment: \n CommentId: " + CommentId + " \nContent: " + Content
            + " \nLike: " + Like + " \nDislike: " + Dislike + " \nCreateDate: " + CreateDate
            + " \nSubcommentId" + SubcommentId + " \nBlogId" + BlogId + " \nUserId" + UserId;
        }
    }
}
