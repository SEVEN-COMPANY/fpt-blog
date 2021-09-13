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
using FPTBlog.Utils;
using Microsoft.AspNetCore.Http;
using FPTBlog.Utils.Interface;

namespace FPTBlog.AuthModule
{

    [Route("auth")]
    public class AuthController : Controller
    {


        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        private readonly IJwtService JwtService;
        public AuthController(IAuthService authService, IJwtService jwtService, IUserService userService)
        {
            this.AuthService = authService;
            this.UserService = userService;
            this.JwtService = jwtService;
        }


        [HttpGet("login")]
        public IActionResult LoginPage()
        {
            return View(Routers.Login.Page);
        }

        [HttpPost("login")]
        public IActionResult LoginHandler(string username, string password)
        {
            var input = new LoginUserDto()
            {
                Username = username,
                Password = password
            };

            ValidationResult result = new LoginUserDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.Login.Page);
            }

            var user = this.UserService.GetUserByUsername(input.Username);
            if (user == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL, this.ViewData);
                return View(Routers.Login.Page);
            }

            var isCorrectPassword = this.AuthService.ComparePassword(input.Password, user.Password);
            if (!isCorrectPassword)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL, this.ViewData);
                return View(Routers.Login.Page);
            }

            var token = this.JwtService.GenerateToken(user.UserId);
            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true

            });

            return Redirect(Routers.Home.Link);
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

            var isExistUser = this.UserService.GetUserByUsername(input.Username);
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
