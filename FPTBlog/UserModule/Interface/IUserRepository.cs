using FPTBlog.UserModule.Entity;

namespace FPTBlog.UserModule.Interface
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);
        public bool SaveUser(User user);
        public User GetUserByUserId(string id);
        public User GetUserByGoogleId(string googleId);
    }
}
