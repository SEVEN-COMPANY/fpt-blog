using System;
using System.Linq;
using System.Collections.Generic;

using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;

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

        #region OK
        public void CreateReward(Reward reward) => this.RewardRepository.Add(reward);
        public Reward GetRewardByRewardId(string rewardId) => this.RewardRepository.Get(rewardId);
        public void GiveUserReward(User user, Reward reward) {
            UserReward userReward = new UserReward() {
                User = user,
                UserId = user.UserId,
                Reward = reward,
                RewardId = reward.RewardId
            };
            var userRewardDb = this.UserRewardRepository.GetFirstOrDefault(item => item.RewardId == userReward.RewardId && item.UserId == userReward.UserId);
            if (userRewardDb == null) {
                this.UserRewardRepository.Add(userReward);
            }
        }
        public void RemoveUserReward(UserReward UserReward) => this.UserRewardRepository.Remove(UserReward.UserRewardId);
        public UserReward GetUserReward(Reward reward, User user) => this.UserRewardRepository.GetFirstOrDefault(item => item.RewardId.Equals(reward.RewardId) && item.UserId.Equals(user.UserId));
        public void UpdateReward(Reward reward) => this.RewardRepository.Update(reward);
        public (List<Reward>, int) GetRewardByName(int pageIndex, int pageSize, string searchName) {
            List<Reward> list = (List<Reward>) this.RewardRepository.GetAll(item => item.Name.Contains(searchName));
            var count = list.Count;
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            return (pagelist, count);
        }

        public Reward GetRewardTypeAndConstraint(RewardType type, int constraint) => this.RewardRepository.GetFirstOrDefault(item => item.Type == type && item.Constraint == constraint);


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

                rewardReport.UserRewards = (List<UserReward>) this.UserRewardRepository.GetAll(item => item.UserId == user.UserId, includeProperties: "Reward");

                rewardReports.Add(rewardReport);
            }

            return (rewardReports, list.Count);
        }

        public UserReward IsUseReward(string rewardId) => this.UserRewardRepository.GetFirstOrDefault(item => item.RewardId.Equals(rewardId));
        public void DeleteReward(string rewardId) => this.RewardRepository.Remove(rewardId);
        public List<Reward> GetRewards() => (List<Reward>) this.RewardRepository.GetAll();

        public List<SelectListItem> GetRewardsDropList() {
            var rewards = new List<SelectListItem>();
            var list = this.RewardRepository.GetAll(item => item.Type == RewardType.Freedom);

            foreach (var item in list) {
                rewards.Add(new SelectListItem() { Value = item.RewardId, Text = item.Name });
            }
            return rewards;
        }

        public List<UserReward> GetUserAllRewards(string userId) {
            return this.UserRewardRepository.GetUserAllRewards(userId);
        }
        #endregion



        public void GiveRewardJob() {
            List<Reward> rewards = this.RewardRepository.GetAll().ToList();
            if (rewards.Count == 0) {
                return;
            }

            List<User> givenRewardUsers = null; // list user được tặng huy hiệu
            User givenRewardUser = null; // user đơn lẻ được tặng huy hiệu
            foreach (var reward in rewards) {
                switch (reward.Type) {
                    case RewardType.Post:
                    givenRewardUsers = this.UserRepository.GetUsersHave_N_Posts(reward.Constraint);
                    foreach (var user in givenRewardUsers) {
                        this.GiveUserReward(user, reward);
                    }
                    break;

                    case RewardType.Viewer_For_A_Post:
                    givenRewardUsers = this.UserRepository.GetUsersHave_N_View_For_A_Post(reward.Constraint);
                    foreach (var user in givenRewardUsers) {
                        this.GiveUserReward(user, reward);
                    }
                    break;

                    case RewardType.Interaction_For_A_Post:
                    givenRewardUsers = this.UserRepository.GetUsersHave_N_Interaction_For_A_Post(reward.Constraint);
                    foreach (var user in givenRewardUsers) {
                        this.GiveUserReward(user, reward);
                    }
                    break;

                    case RewardType.Follower:
                    givenRewardUsers = this.UserRepository.GetUsersHave_N_Followers(reward.Constraint);
                    foreach (var user in givenRewardUsers) {
                        this.GiveUserReward(user, reward);
                    }
                    break;

                    case RewardType.Most_Post_In_N_Month_FromNow:
                    givenRewardUser = this.UserRepository.GetUserHave_Most_Post_In_N_Month_FromNow(reward.Constraint);
                    this.GiveUserReward(givenRewardUser, reward);
                    break;

                    case RewardType.Most_View_For_A_Post_In_N_Month_FromNow:
                    givenRewardUser = this.UserRepository.GetUserHave_Most_View_For_A_Post_In_N_Month_FromNow(reward.Constraint);
                    this.GiveUserReward(givenRewardUser, reward);
                    break;

                    case RewardType.Most_Interaction_For_A_Post_In_N_Month_FromNow:
                    givenRewardUser = this.UserRepository.GetUserHave_Most_Interaction_For_A_Post_In_N_Month_FromNow(reward.Constraint);
                    this.GiveUserReward(givenRewardUser, reward);
                    break;


                }
            }
        }



        public static string ConvertTypeConstranint(RewardType type) {
            switch (type) {
                case RewardType.Post:
                return "Number of post";
                case RewardType.Viewer_For_A_Post:
                return "View of a post";
                case RewardType.Interaction_For_A_Post:
                return "Interaction of a post";
                case RewardType.Follower:
                return "Number of follower";
                case RewardType.Most_Post_In_N_Month_FromNow:
                return "Most post in N month";
                case RewardType.Most_View_For_A_Post_In_N_Month_FromNow:
                return "Most interaction in N month";
                case RewardType.Most_Interaction_For_A_Post_In_N_Month_FromNow:
                return "The best post of a month";
                case RewardType.Freedom:
                return "Free to give";
            }

            return "";
        }

        public List<SelectListItem> GetRewardTypeDropList() {
            var rewardType = new List<SelectListItem>();

            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Post).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Post) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Viewer_For_A_Post).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Viewer_For_A_Post) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Interaction_For_A_Post).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Interaction_For_A_Post) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Follower).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Follower) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Most_Post_In_N_Month_FromNow).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Most_Post_In_N_Month_FromNow) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Most_View_For_A_Post_In_N_Month_FromNow).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Most_View_For_A_Post_In_N_Month_FromNow) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Most_Interaction_For_A_Post_In_N_Month_FromNow).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Most_Interaction_For_A_Post_In_N_Month_FromNow) });
            rewardType.Add(new SelectListItem() { Value = ((int) RewardType.Freedom).ToString(), Text = RewardService.ConvertTypeConstranint(RewardType.Freedom) });


            return rewardType;
        }

        #region  Chart
        public List<RewardChart> GetRewardChart() {
            string thisMonth = DateTime.Now.AddMonths(-1).ToShortDateString();
            DateTime thisMonthDate = Convert.ToDateTime(thisMonth);
            List<RewardChart> chart = new List<RewardChart>();
            var rewards = this.RewardRepository.GetAll();
            foreach (var reward in rewards) {
                var rewardChart = new RewardChart();
                rewardChart.name = reward.Name;
                int total = 0;
                List<UserReward> userRewards = this.UserRewardRepository.GetAll(item => item.RewardId == reward.RewardId).ToList();
                for (int i = userRewards.Count - 1; i >= 0; i--) {
                    DateTime date = Convert.ToDateTime(userRewards[i].CreateDate);
                    if (DateTime.Compare(date, thisMonthDate) < 0) {
                        userRewards.Remove(userRewards[i]);
                    }
                    else {
                        total += 1;
                    }
                }
                rewardChart.total = total;
                chart.Add(rewardChart);

            }
            return chart;
        }
        #endregion
    }
}

