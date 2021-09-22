using System;
using FPTBlog.Src.AuthModule.Interface;


namespace FPTBlog.Src.AuthModule
{
    public class AuthService : IAuthService
    {
        public string HashingPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 8);
        }

        public bool ComparePassword(string inputPassword, string encryptedPassword)
        {
            Console.WriteLine(inputPassword);
            Console.WriteLine(encryptedPassword);
            return BCrypt.Net.BCrypt.Verify(inputPassword, encryptedPassword);
        }
    }
}
