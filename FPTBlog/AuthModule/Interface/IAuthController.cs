
using FPTBlog.AuthModule.DTO;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.AuthModule.Interface
{
    public interface IAuthController
    {
        public ObjectResult RegisterUser(RegisterUserDto body);
    }
}
