using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using System;

namespace FPTBlog.Src.UserModule {
    public class UserRepository : IUserRepository {
        private readonly DB Db;
        public UserRepository(DB db) {
            this.Db = db;
        }


        public User GetUserByUsername(string username) {
            var user = this.Db.User.FirstOrDefault(item => item.Username == username);
            return user;
        }

        public bool SaveUser(User user) {
            this.Db.User.Add(user);
            return this.Db.SaveChanges() > 0;
        }
        public User GetUserByUserId(string id) {
            User user = this.Db.User.FirstOrDefault(item => item.UserId == id);
            return user;
        }

        public User GetUserByGoogleId(string googleId) {
            User user = this.Db.User.FirstOrDefault(item => item.GoogleId == googleId);
            return user;
        }

        public bool UpdateUser(User user) {
            User updateUser = this.GetUserByUserId(user.UserId);

            updateUser.Name = user.Name;
            updateUser.Email = user.Email;
            updateUser.Phone = user.Phone;
            updateUser.Address = user.Address;
            return this.Db.SaveChanges() > 0;
        }

        public bool ChangePasswordHandler(User user) {
            User updateUser = this.GetUserByUserId(user.UserId);
            updateUser.Password = user.Password;
            return this.Db.SaveChanges() > 0;
        }

        public bool BlockUserByAdminHandler(User user) {
            User blockedUser = this.GetUserByUserId(user.UserId);
            blockedUser.Status = 0;
            return this.Db.SaveChanges() > 0;
        }

        public (List<User>, int) GetUsersWithStatus(int pageSize, int pageIndex, UserStatus status, string name) {

            var query = (from User in this.Db.User
                         where User.Name.Contains(name) && User.Status == status
                         select User);
            List<User> users = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            foreach (User user in users) {
                user.Password = "";
            }
            return (users, users.Count);
        }
        public (List<User>, int) GetUsersByPageAndCount(int pageSize, int pageIndex, string search) {
            var query = this.Db.User.Where(x => x.Name.Contains(search) || x.Email.Contains(search));
            List<User> users = query.Take((pageSize + 1) * pageIndex).Skip(pageIndex * pageSize).ToList();
            int count = query.Count();
            return (users, count);
        }
    }
}
