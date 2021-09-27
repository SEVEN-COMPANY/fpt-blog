using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;


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
        public IActionResult GetUsers() {
            var (users, count) = this.UserService.GetUsers();
            this.ViewData["users"] = users;
            this.ViewData["count"] = count;
            return View(RoutersAdmin.GetUsers.Page);
        }

        // [HttpGet("")]
        // public IActionResult GetUser() {
        //     return View(Routers.AdminDashboard.Page);
        // }

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
