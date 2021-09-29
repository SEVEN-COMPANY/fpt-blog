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
    }
}
