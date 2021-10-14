using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.NotificationModule.Entity {
    public enum NotificationLevel {
        INFOMATION = 1,
        WARNING = 2
    }

    [Table("tblNotification")]
    public class Notification {
        [Key]
        [Required]
        [StringLength(40)]
        public string NotificationId {
            get; set;
        }

        [Required]
        public string Content {
            get; set;
        }

        [Required]
        public NotificationLevel Level {
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

        [ForeignKey("tblUser")]
        [StringLength(40)]
        public string ReceiverId {
            get; set;
        }
        public virtual User Receiver {
            get; set;
        }


        public Notification() {
            this.NotificationId = Guid.NewGuid().ToString();
            this.Content = "";
            this.Level = NotificationLevel.INFOMATION;
            this.CreateDate = DateTime.Now.ToShortDateString();
            this.Sender = null;
            this.Receiver = null;
        }

        public override string ToString() {
            return "Notification: \n NotificationId: " + NotificationId + " \nContent: " + Content
            + " \nLevel: " + Level + " \nCreateDate: " + CreateDate
            + " \nSenderId" + SenderId + " \nReceiverId" + ReceiverId;
        }
    }
}
