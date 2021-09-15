using FPTBlog.UserModule.DTO;
using FPTBlog.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FPTBlog.UserModule.Interface
{
    public interface IUserService
    {
        public User GetUserByUsername(string username);
        public bool SaveUser(User user);
        public bool UpdateUserHandler(UpdateUserDto input, ViewDataDictionary dataView);
        public User GetUserByUserId(string id);
        public User GetUserByGoogleId(string googleId);

    }
}
