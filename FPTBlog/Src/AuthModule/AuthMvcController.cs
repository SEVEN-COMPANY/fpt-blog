using System;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.AuthModule.Interface;

using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Src.NotificationModule.Interface;
using System.Collections.Generic;
using FPTBlog.Src.NotificationModule.Entity;
using FPTBlog.Utils.Locale;

namespace FPTBlog.Src.AuthModule {

    [Route("/auth")]
    [ServiceFilter(typeof(UserFilter))]
    public class AuthMvcController : Controller {
        private readonly IAuthService AuthService;
        private readonly IUserService UserService;
        private readonly IJwtService JwtService;
        private readonly INotificationService NotificationService;
        public AuthMvcController(IAuthService authService, IJwtService jwtService, IUserService userService, INotificationService notificationService) {
            this.AuthService = authService;
            this.UserService = userService;
            this.JwtService = jwtService;
            this.NotificationService = notificationService;
        }

        [HttpGet("login")]
        public IActionResult LoginPage() {
            var user = (User) this.ViewData["user"];
            if (user != null) {
                return Redirect(Routers.CommonGetHome.Link);
            }
            return View(Routers.AuthPostLogin.Page);
        }

        [HttpGet("google")]
        public IActionResult LoginGoogle([FromQuery(Name = "credential")] string credential) {
            var res = new ServerApiResponse<User>();
            JwtSecurityToken jwtToken = this.JwtService.Decode(credential);
            string id = (string) this.JwtService.GetDataFromJwtToken(jwtToken, "sub");
            User user = this.UserService.GetUserByGoogleId(id);
            if (user == null) {
                user = new User();
                user.UserId = Guid.NewGuid().ToString();
                user.GoogleId = (string) this.JwtService.GetDataFromJwtToken(jwtToken, "sub");
                user.Name = (string) this.JwtService.GetDataFromJwtToken(jwtToken, "name");
                user.Email = (string) this.JwtService.GetDataFromJwtToken(jwtToken, "email");
                user.AvatarUrl = (string) this.JwtService.GetDataFromJwtToken(jwtToken, "picture");
                user.Role = UserRole.STUDENT;
                this.UserService.AddUser(user);
            }

            var token = this.JwtService.GenerateToken(user.UserId);
            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions() {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true
            });

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

            return Redirect(Routers.CommonGetHome.Link);

        }

        [HttpGet("register")]
        public IActionResult RegisterPage() {
            var user = (User) this.ViewData["user"];
            if (user != null) {
                return Redirect(Routers.CommonGetHome.Link);
            }
            return View(Routers.AuthPostRegister.Page);
        }

        [HttpGet("logout")]
        [ServiceFilter(typeof(AuthGuard))]
        public IActionResult LogoutHandler() {

            var res = new ServerApiResponse<string>();
            this.HttpContext.Response.Cookies.Append("auth-token", "", new CookieOptions() {
                Expires = DateTime.Now.AddDays(-1),
                SameSite = SameSiteMode.None,
                Secure = true

            });

            return Redirect(Routers.AuthPostLogin.Link);
        }
    }
}
