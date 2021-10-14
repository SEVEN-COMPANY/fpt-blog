using System.Collections.Generic;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.RewardModule.Interface {
    public interface IRewardService {
        public void CreateReward(Reward reward);
        public Reward GetRewardByRewardId(string rewardId);
        public void GiveUserReward(UserReward userReward);
        public void RemoveUserReward(UserReward userReward);
        public UserReward GetUserReward(Reward reward, User user);
        public void UpdateReward(Reward reward);
        public (List<Reward>, int) GetRewardByName(int pageIndex, int pageSize, string searchName);
        public UserReward IsUseReward(string rewardId);
        public void DeleteReward(string rewardId);
        public (List<RewardReport>, int) GetRewardReport(string searchName, string startDate, string endDate, int pageSize, int pageIndex);
        public List<Reward> GetRewards();
        public List<SelectListItem> GetRewardsDropList();
        public List<UserReward> GetUserAllRewards(string userId);
        public void GiveRewardForUserHave_N_Posts(int N);
    }
}
