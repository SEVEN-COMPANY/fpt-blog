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

        public Reward() {
            this.RewardId = Guid.NewGuid().ToString();
            this.Name = "";
            this.Description = "";
            this.ImageUrl = "https://picsum.photos/128";
            this.CreateDate = DateTime.Now.ToShortDateString();
        }
    }

    public class RewardName {
        // Student can get this badge if they have the highest interactive post in this month
        public static string Conqueror = "Conqueror";

        // Students can get this badge if they post the most posts in a month
        public static string Spammer = "Spammer";

        // Students can get this badge if they have more than 100 followers
        public static string Attractor = "Attractor";

        // 	Students can get this badge if they have more than 5 posts
        public static string Hunter = "Hunter";

        // Students can get this badge if they have more than 10 posts
        public static string Killer = "Killer";

        // Students can get this badge if they have a post which have most viewers
        public static string Hero = "Hero";

        // Students can get this badge if they have more than 25 posts
        public static string Monster = "Monster";

        // Students can get this badge if they have more than 50 posts
        public static string Terminator = "Terminator";
    }
}
