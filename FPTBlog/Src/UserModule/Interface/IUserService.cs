using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;


namespace FPTBlog.Src.UserModule.Interface
{
    public interface IUserService
    {
        public User GetUserByUsername(string username);
        public bool SaveUser(User user);
        public bool UpdateUser(User user);
        public User GetUserByUserId(string id);
        public User GetUserByGoogleId(string googleId);
        public void ChangePasswordHandler(User user);
        public (List<User>, int) GetUsers();
    }
}
