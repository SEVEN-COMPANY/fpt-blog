using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using FPTBlog.Utils.Repository.Interface;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.UserModule.Interface {
    public interface IUserRepository : IRepository<User> {
        (List<User>, int) GetUsersNameWithCount(int pageSize, int pageIndex, string searchName);
        (List<User>, int) GetUsersStatusAndRoleWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus, UserRole searchRole);
        void FollowUser(User followingUser, User follower);
        public void UnfollowUser(User followingUser, User follower);
        public (List<User>, int) CalculateFollower(string userId);
        public (List<User>, int) CalculateFollowing(string userId);
        public (List<User>, int) GetFollowerForPage(string userId, int pageIndex, int pageSize, string searchName);
        public (List<User>, int) GetFollowingForPage(string userId, int pageIndex, int pageSize, string searchName);
        public bool IsFollow(string userId, string followerId);
        public bool IsSave(string userId, string postId);
        public ReportUser GetMonthlyReport();
        public void SavePost(User user, Post post);
        public void UnsavePost(User user, Post post);
        public (List<Post>, int) GetSavePost(string userId, int pageIndex, int pageSize, string searchTitle, string searchCategoryId);
    }
}
