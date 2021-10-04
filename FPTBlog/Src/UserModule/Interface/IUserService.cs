using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;


namespace FPTBlog.Src.UserModule.Interface {
    public interface IUserService {
        public void AddUser(User user);
        public User GetUserByUserId(string id);
        public User GetUserByUsername(string username);
        public User GetUserByGoogleId(string googleId);
        public void UpdateUser(User user);
        public void RemoveUser(User user);
        public void BlockUserByAdminHandler(User user);
        public (List<User>, int) GetUsersStatusWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus);
        public (List<User>, int) GetUsersWithCount(int pageSize, int pageIndex, string search);
        void FollowUser(User followingUser, User follower);
    }
}
