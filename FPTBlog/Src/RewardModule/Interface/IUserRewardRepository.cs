using System.Collections.Generic;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.RewardModule.Interface {
    public interface IUserRewardRepository : IRepository<UserReward> {
        public (List<UserReward>, int) GetUserReward(int pageIndex, int pageSize, string userId);

    }
}