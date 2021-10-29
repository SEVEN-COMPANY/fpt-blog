using System;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FPTBlog.Src.AuthModule.DTO;
using FPTBlog.Src.AuthModule.Interface;

using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;

using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using FPTBlog.Utils.Common;
using FluentValidation.Results;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.NotificationModule.Entity;
using System.Collections.Generic;

namespace FPTBlog.Src.AuthModule {
    [Route("/api/auth")]

    public class AuthApiController : Controller {

        private readonly IAuthService AuthService;
        private readonly IUserService UserService;
        private readonly IJwtService JwtService;
        private readonly INotificationService NotificationService;
        public AuthApiController(IAuthService authService, IJwtService jwtService, IUserService userService, INotificationService notificationService) {
            this.AuthService = authService;
            this.UserService = userService;
            this.JwtService = jwtService;
            this.NotificationService = notificationService;
        }

        [HttpPost("login")]
        public ObjectResult LoginHandler([FromBody] LoginUserDto body) {
            var res = new ServerApiResponse<User>();

            ValidationResult result = new LoginUserDtoValidator().Validate(body);
            if (!result.IsValid) {

                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = this.UserService.GetUserByUsername(body.Username);
            if (user == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL);
                return new BadRequestObjectResult(res.getResponse());
            }

            if (user.Status == UserStatus.DISABLE) {

                var (notifications, _) = this.NotificationService.GetUserNotification(user.UserId);
                var context = new Dictionary<string, object>();
                for (int i = 0; i < notifications.Count; i++) {
                    var item = notifications[i];
                    if (item.Level == NotificationLevel.BANNED || item.Level == NotificationLevel.WARNING) {
                        context.Add("Reason", item.Content);
                        context.Add("ID", item.NotificationId);
                        break;
                    }
                }
                res.data = user;
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_DISSABLED_ACCOUNT, context);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isCorrectPassword = this.AuthService.ComparePassword(body.Password, user.Password);
            if (!isCorrectPassword) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL);
                return new BadRequestObjectResult(res.getResponse());
            }

            var token = this.JwtService.GenerateToken(user.UserId);
            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions() {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true

            });

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_LOGIN_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("register")]
        public IActionResult RegisterHandler([FromBody] RegisterUserDto body) {
            var res = new ServerApiResponse<string>();

            ValidationResult result = new RegisterUserDtoValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isExistUser = this.UserService.GetUserByUsername(body.Username);
            if (isExistUser != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "username");
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = new User();
            user.UserId = Guid.NewGuid().ToString();
            user.Name = body.Name;
            user.Username = body.Username;
            user.CreateDate = DateTime.Now.ToShortDateString();
            user.Password = this.AuthService.HashingPassword(body.Password);
            this.UserService.AddUser(user);
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_REGISTER_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}
