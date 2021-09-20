using FluentValidation.Results;
using FPTBlog.UserModule.DTO;
using FPTBlog.UserModule.Entity;
using FPTBlog.UserModule.Interface;
using FPTBlog.Utils;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FPTBlog.UserModule
{
    public class UserService : IUserService
    {
        private readonly DB dB;
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository, DB dB)
        {
            this.dB = dB;
            this.UserRepository = userRepository;
        }

        public User GetUserByGoogleId(string googleId)
        {
            return this.UserRepository.GetUserByGoogleId(googleId);
        }

        public User GetUserByUserId(string id)
        {
            return this.UserRepository.GetUserByUserId(id);
        }

        public User GetUserByUsername(string username)
        {
            return this.UserRepository.GetUserByUsername(username);
        }

        public bool SaveUser(User user)
        {
            bool res = this.UserRepository.SaveUser(user);
            return res;
        }

        public bool UpdateUserHandler(UpdateUserDto input, ViewDataDictionary dataView)
        {
            ValidationResult result = new UpdateUserDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, dataView);
                return false;
            }

            User user = (User)dataView["user"];
            user.Name = input.Name;
            user.Email = input.Email;
            user.Address = input.Address;
            user.Phone = input.Phone;
            this.dB.SaveChanges();
            return true;
        }

        public void ChangePasswordHandler(ChangePassDto input, ViewDataDictionary dataView)
        {
            User user = (User)dataView["user"];
            user.Password = input.NewPassword;
            this.dB.SaveChanges();
        }
    }
}
