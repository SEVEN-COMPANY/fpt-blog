using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBlog.AuthModule.Interface
{
    public interface IAuthService
    {
        public string HashingPassword(string password);

        public bool ComparePassword(string inputPassword, string encryptedPassword);
    }
}
