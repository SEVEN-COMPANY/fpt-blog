using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using System.Linq;
namespace FPTBlog.Src.RewardModule {

    public class RewardService : IRewardService {
        private readonly IRewardRepository RewardRepository;
        private readonly IUserRewardRepository UserRewardRepository;
        public RewardService(IRewardRepository rewardRepository, IUserRewardRepository userRewardRepository) {
            this.RewardRepository = rewardRepository;
            this.UserRewardRepository = userRewardRepository;
        }

        public void CreateReward(Reward reward) => this.RewardRepository.Add(reward);
        public Reward GetRewardByRewardId(string rewardId) => this.RewardRepository.Get(rewardId);
        public void GiveUserReward(UserReward userReward) => this.UserRewardRepository.Add(userReward);
        public void RemoveUserReward(UserReward UserReward) => this.UserRewardRepository.Remove(UserReward.UserRewardId);
        public UserReward GetUserReward(Reward reward, User user) => this.UserRewardRepository.GetFirstOrDefault(item => item.RewardId.Equals(reward.RewardId) && item.UserId.Equals(user.UserId));
        public void UpdateReward(Reward reward) => this.RewardRepository.Update(reward);
        public (List<Reward>, int) GetRewardByName(int pageIndex, int pageSize, string searchName) {
            List<Reward> list = (List<Reward>) this.RewardRepository.GetAll(item => item.Name.Contains(searchName));
            var count = list.Count;
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            return (pagelist, count);
        }
        public Reward GetRewardById(string id) {
            Reward reward = (Reward) this.RewardRepository.Get(id);

            return reward;
        }
    }
}
