using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.UserModule.Interface {
    public interface IUserService {
        public void AddUser(User user);
        public User GetUserByUserId(string id);
        public User GetUserByUsername(string username);
        public User GetUserByGoogleId(string googleId);
        public void UpdateUser(User user);
        public void RemoveUser(User user);
        public void ToggleUserStatusAdminHandler(User user);
        public void ToggleUserRoleAdminHandler(User user);
        public (List<User>, int) GetUsersStatusAndRoleWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus, UserRole searchRole);
        public (List<User>, int) GetUsersNameWithCount(int pageSize, int pageIndex, string search);
        public void FollowUser(User followingUser, User follower);
        public void UnfollowUser(User followingUser, User follower);
        public (List<User>, int) CalculateFollower(string userId);
        public (List<User>, int) CalculateFollowing(string userId);
        public (List<User>, int) GetFollowerForPage(string userId, int pageIndex, int pageSize, string searchName);
        public (List<User>, int) GetFollowingForPage(string userId, int pageIndex, int pageSize, string searchName);
        public bool IsFollow(string userId, string followerId);
        public bool IsSave(string userId, string postId);
        public List<SelectListItem> GetUserStatusDropList();
        public List<SelectListItem> GetUserRoleDropList();
        public int CountUserByRole(UserRole role);
        public ReportUser GetMonthlyReport();
        public void SavePost(User user, Post post);
        public void UnsavePost(User user, Post post);
        public (List<Post>, int) GetSavePost(string userId, int pageIndex, int pageSize, string searchTitle, string searchCategoryId);

        public List<User> GetUsersHave_N_Posts(int N);
        public List<User> GetUsersHave_N_Followers(int N);
        public (int, int) GetUserChartInformation();
    }
}
