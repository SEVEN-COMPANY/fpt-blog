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
        public IActionResult GetProfile(string searchName, string searCategoryId) {
            var user = (User) this.ViewData["user"];



            // them dem like, dem follower, dem post,   keo ve het cacs post cua use, các bài post trong
            // profile sẽ phân trang, có search theo tên, categoryId
            this.ViewData["profile"] = user;
            return View(Routers.UserGetMyProfile.Page);
        }

        //

        // 1 router profile lay thong tin cua 1 thang user, truyen userId

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
