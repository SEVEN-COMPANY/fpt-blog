using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;

namespace FPTBlog.Src.RewardModule {
    public class UserRewardRepository : Repository<UserReward>, IUserRewardRepository {
        private readonly DB DB;
        public UserRewardRepository(DB dB) : base(dB) {
            this.DB = dB;
        }
    }
}