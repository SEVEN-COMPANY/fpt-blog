using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using System;
using FPTBlog.Utils.Repository;
namespace FPTBlog.Src.UserModule {
    public class UserRepository : Repository<User>, IUserRepository {
        private readonly DB Db;
        public UserRepository(DB db) : base(db) {
            this.Db = db;
        }

        public (List<User>, int) GetUsersStatusWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus) {
            List<User> list = (List<User>) this.GetAll(item => item.Name.Contains(searchName) && item.Status == searchStatus);
            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();


            return (list, count);
        }
        public (List<User>, int) GetUsersWithCount(int pageSize, int pageIndex, string searchName) {
            List<User> list = (List<User>) this.GetAll(item => item.Name.Contains(searchName));
            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();


            return (list, count);
        }
    }
}
