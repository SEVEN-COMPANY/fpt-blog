using System.Collections.Generic;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.RewardModule.Interface {
    public interface IUserRewardRepository : IRepository<UserReward> {
        public List<UserReward> GetUserAllRewards(string userId);

    }
}