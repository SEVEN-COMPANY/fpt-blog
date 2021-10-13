using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.RewardModule {

    public class RewardService : IRewardService {
        private readonly IRewardRepository RewardRepository;
        private readonly IUserRewardRepository UserRewardRepository;
        private readonly IPostRepository PostRepository;
        private readonly IUserRepository UserRepository;
        public RewardService(IPostRepository postRepository, IUserRepository userRepository, IRewardRepository rewardRepository, IUserRewardRepository userRewardRepository) {
            this.RewardRepository = rewardRepository;
            this.UserRewardRepository = userRewardRepository;
            this.UserRepository = userRepository;
            this.PostRepository = postRepository;
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

        public (List<RewardReport>, int) GetRewardReport(string searchName, string startDate, string endDate, int pageSize, int pageIndex) {
            DateTime startDateTime = Convert.ToDateTime(startDate);
            DateTime endDateTime = Convert.ToDateTime(endDate);
            List<RewardReport> rewardReports = new List<RewardReport>();
            List<User> list = (List<User>) this.UserRepository.GetAll(item => item.Name.Contains(searchName));

            List<User> users = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            foreach (User user in users) {
                RewardReport rewardReport = new RewardReport();
                rewardReport.Name = user.Name;
                rewardReport.UserId = user.UserId;

                List<Post> posts = (List<Post>) this.PostRepository.GetAll(item => item.StudentId == user.UserId);
                foreach (Post post in posts) {
                    DateTime date = Convert.ToDateTime(post.CreateDate);
                    if (DateTime.Compare(date, startDateTime) > 0 && (DateTime.Compare(date, endDateTime) < 0)) {
                        rewardReport.TotalPost++;
                        rewardReport.ToTalView += post.View;
                        rewardReport.TotalInteraction += (post.Like + post.Dislike);
                    }
                }

                rewardReport.UserRewards = (List<UserReward>) this.UserRewardRepository.GetAll(item => item.UserId == user.UserId);

                rewardReports.Add(rewardReport);
            }

            return (rewardReports, list.Count);
        }


        public UserReward IsUseReward(string rewardId) => this.UserRewardRepository.GetFirstOrDefault(item => item.RewardId.Equals(rewardId));
        public void DeleteReward(string rewardId) => this.RewardRepository.Remove(rewardId);
        public List<Reward> GetRewards() => (List<Reward>) this.RewardRepository.GetAll();

        public List<SelectListItem> GetRewardsDropList() {
            var rewards = new List<SelectListItem>();
            var list = this.RewardRepository.GetAll();
            foreach (var item in list) {
                rewards.Add(new SelectListItem() { Value = item.RewardId, Text = item.Name });
            }
            return rewards;
        }

        public List<UserReward> GetUserAllRewards(string userId) {
            return this.UserRewardRepository.GetUserAllRewards(userId);
        }
    }
}
