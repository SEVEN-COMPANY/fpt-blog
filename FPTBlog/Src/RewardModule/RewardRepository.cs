using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.RewardModule {
    public class RewardRepository : Repository<Reward>, IRewardRepository {
        private readonly DB DB;
        public RewardRepository(DB dB) : base(dB) {
            this.DB = dB;
        }
    }
}