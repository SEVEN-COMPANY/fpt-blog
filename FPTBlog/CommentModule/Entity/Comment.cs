using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using FPTBlog.BlogModule.Entity;
using FPTBlog.UserModule.Entity;

namespace FPTBlog.CommentModule.Entity
{
    [Table("tblComment")]
    public class Comment
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string CommentId { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Like { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Dislike { get; set; }

        [Required]
        [StringLength(20)]
        public string CreateDate { get; set; }

        [ForeignKey("tblBlog")]
        [Required]
        [StringLength(40)]
        public string BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [ForeignKey("tblUser")]
        [Required]
        [StringLength(40)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("tblComment")]
        [StringLength(40)]
        public string SubCommentId { get; set; }
        public virtual Comment SubComment { get; set; }

        public Comment()
        {
            this.CommentId = Guid.NewGuid().ToString();
            this.Content = "";
            this.Like = 0;
            this.Dislike = 0;
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.Blog = null;
            this.User = null;
            this.SubComment = null;
        }
    }
}