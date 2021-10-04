using System;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;

namespace FPTBlog.Src.UserModule {
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserMvcController : Controller {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public UserMvcController(IUserService UserService) {
            this.UserService = UserService;
        }

        [HttpGet("")]
        public IActionResult GetUser() {

            return View(Routers.UserGetProfile.Page);
        }


        [HttpGet("me")]
        public IActionResult GetProfile() {
            var user = (User) this.ViewData["user"];

            this.ViewData["profile"] = user;
            return View(Routers.UserGetMyProfile.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateUser() {
            return View(Routers.UserPutUser.Page);
        }


        [HttpGet("change-password")]
        public IActionResult ChangePassPage() {
            return View(Routers.UserPutPassword.Page);
        }


    }
}
