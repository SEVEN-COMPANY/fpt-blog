using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;

namespace FPTBlog.Src.UserModule.Interface {
    public interface IUserRepository {
        public User GetUserByUsername(string username);
        public bool SaveUser(User user);
        public User GetUserByUserId(string id);
        public User GetUserByGoogleId(string googleId);
        public bool UpdateUser(User user);
        public bool ChangePasswordHandler(User user);
        public bool BlockUserByAdminHandler(User user);
        public (List<User>, int) GetUsersWithStatus(int pageSize, int pageIndex, UserStatus status, string name);
        public (List<User>, int) GetUsersByPageAndCount(int pageSize, int pageIndex, string search);

    }
}
