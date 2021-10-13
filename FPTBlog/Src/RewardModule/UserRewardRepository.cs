using System.Collections.Generic;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Utils.Repository;
using System.Linq;
namespace FPTBlog.Src.RewardModule {
    public class UserRewardRepository : Repository<UserReward>, IUserRewardRepository {
        private readonly DB Db;
        public UserRewardRepository(DB db) : base(db) {
            this.Db = db;
        }

        public List<UserReward> GetUserAllRewards(string userId) {
            var query = (from UserReward in this.Db.UserReward
                         where (UserReward.UserId.Equals(userId))
                         select UserReward);
            List<UserReward> list = query.ToList();
            foreach (var userReward in list) {
                this.Db.Entry(userReward).Reference(item => item.Reward).Load();
            }
            return list;
        }

    }
}