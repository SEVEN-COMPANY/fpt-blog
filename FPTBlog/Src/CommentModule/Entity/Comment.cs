using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.PostModule.Entity;

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
        public string CreateDate {
            get; set;
        }

        [ForeignKey("tblComment")]
        [StringLength(50)]
        public string OriCommentId {
            get; set;
        }

        /*
        select min(salary)
        from employee
        group by roomId
        */

        public virtual Comment OriComment {
            get; set;
        }

        [ForeignKey("tblPost")]
        [StringLength(40)]
        public string PostId {
            get; set;
        }
        public virtual Post Post {
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
            this.CreateDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            this.OriCommentId = null;
            this.PostId = null;
            this.UserId = null;
        }

        public override string ToString() {
            return "Comment: \n CommentId: " + CommentId + " \nContent: " + Content
            + " \nLike: " + Like + " \nDislike: " + Dislike + " \nCreateDate: " + CreateDate
            + " \nSubcommentId" + OriCommentId + " \nBlogId" + PostId + " \nUserId" + UserId;
        }
    }
}
