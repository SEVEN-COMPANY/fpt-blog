using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBlog.Src.UserModule.Entity
{
    [Table("tblFollowInfo")]
    public class FollowInfo
    {
        [Key]
        public string FollowInfoId{
            get;set;
        }

        [Required]
        public string FollowingUserId {
            get; set;
        }

        [ForeignKey("FollowingUserId")]
        public User FollowingUser {
            get; set;
        }

        [Required]
        public string FollowerId {
            get; set;
        }

        [ForeignKey("FollowerId")]
        public User Follower {
            get; set;
        }
        public FollowInfo()
        {
            this.FollowInfoId = Guid.NewGuid().ToString();
        }
    }
}
