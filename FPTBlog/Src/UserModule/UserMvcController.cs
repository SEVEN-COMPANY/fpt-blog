using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Utils.Locale;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FluentValidation.Results;

namespace FPTBlog.Src.UserModule
{
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserMvcController : Controller
    {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public UserMvcController(IUserService UserService)
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
                ServerMvcResponse.MapDetails(result, this.ViewData);
                return View(Routers.ChangePass.Page);
            }

            if (user == null)
            {
                ServerMvcResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL, this.ViewData);
                return Redirect(Routers.Login.Link);
            }

            var isCorrectPassword = this.AuthService.ComparePassword(oldPassword, user.Password);
            if (!isCorrectPassword)
            {
                ServerMvcResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_OLD_PASSWORD_IS_WRONG, this.ViewData);
                return Redirect(Routers.ChangePass.Page);
            }
            user.Password = input.NewPassword;
            var isUpdate = this.UserService.ChangePasswordHandler(user);
            if (!isUpdate)
            {
                return View(Routers.ChangePass.Page);
            }
            ServerMvcResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS, this.ViewData);
            return Redirect(Routers.Login.Link);
        }
    }
}