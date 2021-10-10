using System.Collections.Generic;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.RewardModule.Interface {
    public interface IRewardService {
        public void CreateReward(Reward reward);
        public Reward GetRewardByRewardId(string rewardId);
        public void GiveUserReward(UserReward userReward);
        public void RemoveUserReward(UserReward userReward);
        public UserReward GetUserReward(Reward reward, User user);
        public void UpdateReward(Reward reward);
        public (List<Reward>, int) GetRewardByName(int pageIndex, int pageSize, string searchName);
        public Reward GetRewardById(string id);
        public (List<UserReward>, int) GetUserReward(int pageIndex, int pageSize, string userId);

    }
}
