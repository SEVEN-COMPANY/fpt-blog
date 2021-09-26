using FluentValidation.Results;
using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;


namespace FPTBlog.Src.UserModule {
    public class UserService : IUserService {
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository) {

            this.UserRepository = userRepository;
        }

        public User GetUserByGoogleId(string googleId) {
            return this.UserRepository.GetUserByGoogleId(googleId);
        }

        public User GetUserByUserId(string id) {
            return this.UserRepository.GetUserByUserId(id);
        }

        public User GetUserByUsername(string username) {
            return this.UserRepository.GetUserByUsername(username);
        }

        public bool SaveUser(User user) {
            bool res = this.UserRepository.SaveUser(user);
            return res;
        }

        public bool UpdateUser(User user) {
            return this.UserRepository.UpdateUser(user);
        }

        public void ChangePasswordHandler(User user) {
            this.UserRepository.ChangePasswordHandler(user);
        }

        public void BlockUserByAdminHandler(User user) {
            User blockedUser = this.UserRepository.GetUserByUserId(user.UserId);
            this.UserRepository.BlockUserByAdminHandler(blockedUser);
        }

        public (List<User>, int) GetUsers() {
            return this.UserRepository.GetUsers();
        }
        public (List<User>, int) GetUsersByPageAndCount(int currentPage, int pageSize, string search) {
            return this.UserRepository.GetUsersByPageAndCount(currentPage, pageSize, search);
        }
    }
}
