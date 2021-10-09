using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.UserModule.Entity
{
    [Table("tblSavePost")]
    public class SavePost
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string SavePostId{
            get;set;
        }

        [Required]
        public string UserId {
            get; set;
        }

        [ForeignKey("UserId")]
        public User User {
            get; set;
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
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }
        public SavePost()
        {
            this.SavePostId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }
}
