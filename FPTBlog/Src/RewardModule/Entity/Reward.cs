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
        [StringLength(40)]
        public string Name {
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

        [Required]
        public RewardType Type {
            get; set;
        }

        [Required]
        public int Constraint {
            get; set;
        }
        public Reward() {
            this.RewardId = Guid.NewGuid().ToString();
            this.Name = "";
            this.Description = "";
            this.Constraint = 0;
            this.ImageUrl = "https://picsum.photos/128";
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }

    public enum RewardType {
        Post = 1,
        Viewer_For_A_Post = 2,
        Interaction_For_A_Post = 3,
        Follower = 4,
        Most_Post_In_N_Month_FromNow = 5,
        Most_View_For_A_Post_In_N_Month_FromNow = 6,
        Most_Interaction_For_A_Post_In_N_Month_FromNow = 7,
        Freedom = 8
    }
}
