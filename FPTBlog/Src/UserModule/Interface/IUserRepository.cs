using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.UserModule.Interface {
    public interface IUserRepository : IRepository<User> {
        (List<User>, int) GetUsersNameWithCount(int pageSize, int pageIndex, string searchName);
        (List<User>, int) GetUsersStatusAndRoleWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus, UserRole searchRole);
        void FollowUser(User followingUser, User follower);
        public (List<User>, int) CalculateFollower(string userId);
        public (List<User>, int) CalculateFollowing(string userId);
        public bool IsFollow(string userId, string followerId);
        public ReportUser GetMonthlyReport();
    }
}
