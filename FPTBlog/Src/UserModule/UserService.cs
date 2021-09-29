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
            User blockedUser = this.GetUserByUserId(user.UserId);
            blockedUser.Status = 0;
            this.UserRepository.Update(blockedUser);
        }

        public (List<User>, int) GetUsersWithStatus(int pageIndex, int pageSize, string searchName, UserStatus searchStatus) {
            List<User> list = (List<User>) this.UserRepository.GetAll(item => item.Name.Equals(searchName) && item.Status == searchStatus);
            var count = list.Count;
            list = (List<User>) list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize);

            return (list, count);
        }
        public (List<User>, int) GetUsersWithCount(int pageSize, int pageIndex, string searchName) {
            List<User> list = (List<User>) this.UserRepository.GetAll(item => item.Name.Equals(searchName));
            var count = list.Count;
            list = (List<User>) list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize);

            return (list, count);
        }
    }
}
