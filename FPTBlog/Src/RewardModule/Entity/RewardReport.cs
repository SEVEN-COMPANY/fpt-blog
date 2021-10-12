namespace FPTBlog.Src.RewardModule.Entity {
    public class RewardReport {

        public RewardReport() {
            this.UserId = "";
            this.Name = "";
            this.TotalPost = 0;
            this.TotalInteraction = 0;
            this.ToTalView = 0;
        }

        public string UserId {
            get; set;
        }
        public string Name {
            get; set;
        }

        public int TotalPost {
            get; set;
        }

        public int TotalInteraction {
            get; set;
        }

        public int ToTalView {
            get; set;
        }
    }
}