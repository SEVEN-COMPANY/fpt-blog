using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult GetUsers(string searchName, UserStatus searchStatus = UserStatus.ENABLE, int pageSize = 12, int pageIndex = 0) {
            if (searchName == null) {
                searchName = "";
            }

            this.ViewData["status"] = new SelectList(this.UserService.GetUserStatusDropList(), UserStatus.ENABLE);

            var statusList = this.UserService.GetUserStatusDropList();
            statusList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList list = new SelectList(statusList, "");

            this.ViewData["statusSearch"] = list;

            var (users, total) = this.UserService.GetUsersStatusWithCount(pageIndex, pageSize, searchName, searchStatus);
            this.ViewData["users"] = users;
            this.ViewData["total"] = total;
            return View(RoutersAdmin.UserGetUserList.Page);
        }

        [HttpGet("profile")]
        public IActionResult Profile() {
            return View(RoutersAdmin.UserProfile.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateUser() {
            return View(RoutersAdmin.UserPutUser.Page);
        }

        [HttpGet("change-password")]
        public IActionResult ChangePassPage() {
            return View(RoutersAdmin.UserPutPassword.Page);
        }

    }
}
