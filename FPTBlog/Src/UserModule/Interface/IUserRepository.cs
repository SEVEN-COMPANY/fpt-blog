using FPTBlog.Src.UserModule.Entity;
using System.Collections.Generic;
using FPTBlog.Utils.Repository.Interface;

namespace FPTBlog.Src.UserModule.Interface {
    public interface IUserRepository : IRepository<User> {
        (List<User>, int) GetUsersWithCount(int pageSize, int pageIndex, string searchName);
        (List<User>, int) GetUsersStatusWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus);
    }
}
