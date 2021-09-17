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
using FPTBlog.AuthModule;
using FPTBlog.UserModule.DTO;

namespace FPTBlog.UserModule
{
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserController : Controller
    {
        private readonly IAuthService AuthService;
        
        private readonly IUserService UserService;
        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet("")]
        public IActionResult GetUser()
        {
            return View(Routers.User.Page);
        }


        [HttpGet("update")]
        public IActionResult UpdateUser()
        {
            return View(Routers.UpdateUser.Page);
        }

        [HttpPost("update")]
        public IActionResult UpdateUserHandler(string name, string email, string address, string phone, string avartarUrl)
        {
            var input = new UpdateUserDto()
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address
            };

            var isUpdate = this.UserService.UpdateUserHandler(input, this.ViewData);
            if (!isUpdate)
            {
                return View(Routers.UpdateUser.Page);
            }
            return Redirect(Routers.User.Link);
        }


        [HttpGet("change-password")]
        public IActionResult ChangePass()
        {
            return View(Routers.ChangePass.Page);
        }

        [HttpPost("change-password")]
        public IActionResult ChangePasswordHandler(string oldPassword, string newPassword, string confirmNewPassword)
        {
            User user = (User)this.ViewData["user"];
            var input = new ChangePassDto()
            {
                Username = user.Username,
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmNewPassword = confirmNewPassword
            };

            ValidationResult result = new ChangePassDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.ChangePass.Page);
            }

            if(user == null)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL, this.ViewData);
                return Redirect(Routers.Login.Link);
            }

            var isCorrectPassword = this.AuthService.ComparePassword(oldPassword, user.Password);
            if (!isCorrectPassword)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_OLD_PASSWORD_IS_WRONG, this.ViewData);
                return Redirect(Routers.ChangePass.Page);
            }

            var isChange = this.UserService.ChangePasswordHandler(input, this.ViewData);
            if (!isChange)
            {
                return View(Routers.ChangePass.Page);
            }
            return Redirect(Routers.Login.Link);
        }
    }
}