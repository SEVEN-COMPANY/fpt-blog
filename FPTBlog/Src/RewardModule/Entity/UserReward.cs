using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.RewardModule.Entity {

    [Table("tblUserReward")]
    public class UserReward {
        [Key]
        [Required]
        [StringLength(40)]
        public string UserRewardId {
            get; set;
        }

        [Required]
        [StringLength(40)]
        public string UserId {
            get; set;
        }

        [ForeignKey("UserId")]
        public virtual User User {
            get; set;
        }

        [Required]
        [StringLength(40)]
        public string RewardId {
            get; set;
        }

        [ForeignKey("RewardId")]
        public virtual Reward Reward {
            get; set;
        }

        public string CreateDate {
            get; set;
        }

        public UserReward() {
            this.RewardId = Guid.NewGuid().ToString();
            this.UserId = "";
            this.RewardId = "";
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }
}