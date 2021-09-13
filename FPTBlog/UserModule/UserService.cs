using FPTBlog.UserModule.Entity;
using FPTBlog.UserModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBlog.UserModule
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository)
        {

            this.UserRepository = userRepository;
        }
        public User GetUserByUsername(string username)
        {
            User user = this.UserRepository.GetUserByUsername(username);
            return user;
        }

        public bool SaveUser(User user)
        {
            bool res = this.UserRepository.SaveUser(user);
            return res;
        }
    }
}
