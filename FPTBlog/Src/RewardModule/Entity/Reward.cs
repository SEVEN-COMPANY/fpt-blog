using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPTBlog.Src.RewardModule.Entity {
    [Table("tblReward")]
    public class Reward {
        [Key]
        [Required]
        [StringLength(40)]
        public string RewardId {
            get; set;
        }

        [Required]
        [StringLength(500)]
        public string Description {
            get; set;
        }

        [Required]
        public string ImageUrl {
            get; set;
        }

        [Required]
        public string CreateDate {
            get; set;
        }

        public Reward() {
            this.RewardId = Guid.NewGuid().ToString();
            this.Description = "";
            this.ImageUrl = "https://picsum.photos/128";
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }
}