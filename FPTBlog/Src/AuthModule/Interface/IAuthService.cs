

namespace FPTBlog.Src.AuthModule.Interface {
    public interface IAuthService {
        public string HashingPassword(string password);

        public bool ComparePassword(string inputPassword, string encryptedPassword);
    }
}
