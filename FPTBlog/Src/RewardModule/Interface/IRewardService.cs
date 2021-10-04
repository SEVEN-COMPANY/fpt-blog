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
    }
}