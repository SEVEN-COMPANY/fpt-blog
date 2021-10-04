using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using System.Collections.Generic;
using System.Linq;

namespace FPTBlog.Src.UserModule {
    public class UserService : IUserService {
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository) {

            this.UserRepository = userRepository;
        }

        public void AddUser(User user) => this.UserRepository.Add(user);
        public User GetUserByUserId(string id) => this.UserRepository.Get(id);
        public User GetUserByGoogleId(string googleId) => this.UserRepository.GetFirstOrDefault(item => item.GoogleId.Equals(googleId));
        public User GetUserByUsername(string username) => this.UserRepository.GetFirstOrDefault(item => item.Username.Equals(username));
        public void UpdateUser(User user) => this.UserRepository.Update(user);
        public void RemoveUser(User user) => this.UserRepository.Remove(user);

        public void BlockUserByAdminHandler(User user) {
            if (user.Status == UserStatus.ENABLE)
                user.Status = UserStatus.DISABLE;
            else
                user.Status = UserStatus.ENABLE;
            this.UserRepository.Update(user);
        }

        public (List<User>, int) GetUsersStatusWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus) => this.UserRepository.GetUsersStatusWithCount(pageIndex, pageSize, searchName, searchStatus);
        public (List<User>, int) GetUsersWithCount(int pageSize, int pageIndex, string searchName) => this.UserRepository.GetUsersWithCount(pageSize, pageIndex, searchName);
    }
}
