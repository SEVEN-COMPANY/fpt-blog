using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using System;

namespace FPTBlog.Src.UserModule {

    [Route("admin/user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class AdminMvcController : Controller {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public AdminMvcController(IUserService userService, IAuthService authService) {
            this.UserService = userService;
            this.AuthService = authService;
        }

        [HttpGet("list")]
        public IActionResult GetUsers(UserStatus status, string name, int pageSize = 12, int pageIndex = 0) {
            if (status == 0) {
                status = UserStatus.ENABLE;
            }

            if (name == null) {
                name = "";
            }

            var (users, count) = this.UserService.GetUsersWithStatus(pageSize, pageIndex, status, name);
            this.ViewData["users"] = users;
            this.ViewData["count"] = count;
            return View(RoutersAdmin.GetUsers.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateUser() {
            return View(RoutersAdmin.UpdateUser.Page);
        }

        [HttpGet("change-password")]
        public IActionResult ChangePassPage() {
            return View(RoutersAdmin.ChangePassword.Page);
        }

    }
}
