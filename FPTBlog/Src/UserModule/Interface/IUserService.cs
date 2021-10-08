using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;


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
        void FollowUser(User followingUser, User follower);
        public (List<User>, int) CalculateFollower(string userId);
        public (List<User>, int) CalculateFollowing(string userId);

        public bool IsFollow(string userId, string followerId);

        public List<SelectListItem> GetUserStatusDropList();
        public List<SelectListItem> GetUserRoleDropList();
        public int CountUserByRole(UserRole role);
    }
}
