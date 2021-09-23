using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Src.UserModule.Entity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
        public void BlockUserByAdminHandler(User user);

    }
}
