using FPTBlog.UserModule.Entity;
using FPTBlog.UserModule.Interface;
using FPTBlog.Utils;
using System.Linq;

namespace FPTBlog.UserModule
{
    public class UserRepository : IUserRepository
    {
        private readonly DB Db;
        public UserRepository(DB db)
        {
            this.Db = db;
        }


        public User GetUserByUsername(string username)
        {
            var user = this.Db.User.FirstOrDefault(item => item.Username == username);
            return user;
        }

        public bool SaveUser(User user)
        {
            this.Db.User.Add(user);
            return this.Db.SaveChanges() > 0;
        }
        public User GetUserByUserId(string id)
        {
            User user = this.Db.User.FirstOrDefault(item => item.UserId == id);
            return user;
        }

        public User GetUserByGoogleId(string googleId)
        {
            User user = this.Db.User.FirstOrDefault(item => item.GoogleId == googleId);
            return user;
        }
    }
}
