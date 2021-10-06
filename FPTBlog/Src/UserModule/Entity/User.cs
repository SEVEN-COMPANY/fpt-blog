using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBlog.Src.UserModule.Entity {
    public enum UserRole {
        STUDENT = 0,
        LECTURER = 1,
        GUEST = 2
    }

    public enum UserStatus {
        DISABLE = 2,
        ENABLE = 1
    }

    [Table("tblUser")]
    public class User {
        [Key]
        [Required]
        [StringLength(40)]
        public string UserId {
            get; set;
        }

        [StringLength(30)]
        public string GoogleId {
            get; set;
        }

        [StringLength(30)]
        public string Username {
            get; set;
        }

        [StringLength(255)]
        public string Password {
            get; set;
        }

        [Required]
        [StringLength(255)]
        public string Email {
            get; set;
        }

        [Required]
        [StringLength(50)]
        public string Name {
            get; set;
        }

        [Required]
        [StringLength(20)]
        public string Phone {
            get; set;
        }

        [Required]
        [StringLength(255)]
        public string Address {
            get; set;
        }

        [Required]
        public string AvatarUrl {
            get; set;
        }

        [Required]
        public UserStatus Status {
            get; set;
        }
        [Required]
        public UserRole Role {
            get; set;
        }

        [Required]
        [StringLength(20)]
        public string CreateDate {
            get; set;
        }

        public User() {
            this.UserId = Guid.NewGuid().ToString();
            this.GoogleId = "";
            this.Username = "";
            this.Password = "";
            this.Email = "";
            this.Name = "";
            this.Phone = "";
            this.Address = "";
            this.AvatarUrl = "https://picsum.photos/128";
            this.Status = UserStatus.ENABLE;
            this.Role = UserRole.STUDENT;
            this.CreateDate = DateTime.Now.ToShortDateString();
        }

        public override string ToString() {
            return "User: \nUserId: " + UserId + " \nUsername: " + Username + " \nPassword: " + Password + " \nName: " +
                            Name + " \nEmail: " + Email + " \nPhone: " + Phone + " \nAddress: " + Address + " \nGoogleId: " +
                            GoogleId + " \nCreateDate: " + CreateDate + " \nRole: " + Role + " \nStatus: " + Status
                            + " \nAvatarUrl: " + AvatarUrl;
        }
    }
}
