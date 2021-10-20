using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.ChatModule.Entity {
    [Table("tblMessage")]
    public class Message {
        [Key]
        [Required]
        [StringLength(50)]
        public string MessageId {
            get; set;
        }

        [Required]
        public string Content {
            get; set;
        }

        [Required]
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }

        [ForeignKey("tblUser")]
        [StringLength(40)]
        public string SenderId {
            get; set;
        }
        public virtual User Sender {
            get; set;
        }


        public Message() {
            this.MessageId = Guid.NewGuid().ToString();
            this.Content = "";
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.SenderId = null;
        }
    }
}