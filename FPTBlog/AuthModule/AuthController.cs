using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.AuthModule.DTO;
using System.Threading.Tasks;
using FluentValidation.Results;
using FPTBlog.UserModule.Interface;
using FPTBlog.Utils.Locale;
using FPTBlog.UserModule.Entity;
using FPTBlog.AuthModule.Interface;

namespace FPTBlog.AuthModule
{

    [Route("auth")]
    public class AuthController : Controller
    {


        private readonly IUserRepository UserRepository;

        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public AuthController(IUserRepository userRepository, IAuthService authService, IUserService userService)
        {
            this.UserRepository = userRepository;
            this.AuthService = authService;
            this.UserService = userService;
        }


        [HttpGet("login")]
        public IActionResult LoginPage()
        {
            return View(Routers.Login.Page);
        }


        [HttpGet("register")]
        public IActionResult RegisterPage()
        {
            return View(Routers.Register.Page);
        }

        [HttpPost("register")]
        public IActionResult RegisterHandler(string name, string username, string password, string confirmPassword)
        {
            var input = new RegisterUserDto()
            {
                Name = name,
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            ValidationResult result = new RegisterUserDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.Register.Page);
            }

            var isExistUser = this.UserRepository.GetUserByUsername(input.Username);
            if (isExistUser != null)
            {
                ServerResponse.SetFieldErrorMessage("username", CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, this.ViewData);
                return View(Routers.Register.Page);
            }

            var user = new User();
            user.UserId = Guid.NewGuid().ToString();
            user.Name = input.Name;
            user.Username = input.Username;
            user.CreateDate = DateTime.Now.ToShortDateString();
            user.Password = this.AuthService.HashingPassword(input.Password);
            this.UserService.SaveUser(user);

            return Redirect(Routers.Login.Link);
        }
    }
}
