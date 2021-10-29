using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

using FPTBlog.Utils.Common;

using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.NotificationModule.Interface;

namespace FPTBlog.Src.UserModule {

    [Route("admin/user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class AdminMvcController : Controller {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        private readonly INotificationService NotificationService;
        public AdminMvcController(IUserService userService, IAuthService authService, INotificationService notificationService) {
            this.UserService = userService;
            this.AuthService = authService;
            this.NotificationService = notificationService;
        }

        [HttpGet("list")]
        public IActionResult GetUsers(string searchName, UserStatus searchStatus, UserRole searchRole, int pageSize = 12, int pageIndex = 0) {
            if (searchName == null) {
                searchName = "";
            }


            this.ViewData["notificationLevel"] = new SelectList(this.NotificationService.GetNotificationNegativeLevelDropList(), "");

            // get status user for update user status
            this.ViewData["status"] = new SelectList(this.UserService.GetUserStatusDropList(), UserStatus.ENABLE);
            // get status search list for search by status
            var statusList = this.UserService.GetUserStatusDropList();
            statusList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList listStatus = new SelectList(statusList, "");
            this.ViewData["statusSearch"] = listStatus;

            // get role user for update user role
            this.ViewData["role"] = new SelectList(this.UserService.GetUserRoleDropList(), UserRole.STUDENT);
            // get role search list for search by role
            var roleList = this.UserService.GetUserRoleDropList();
            roleList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList listRole = new SelectList(roleList, "");

            int totalStudent = this.UserService.CountUserByRole(UserRole.STUDENT);
            int totalLecturer = this.UserService.CountUserByRole(UserRole.LECTURER);

            var monthlyReport = this.UserService.GetMonthlyReport();

            this.ViewData["studentThisMonth"] = monthlyReport.StudentThisMonth;
            this.ViewData["studentLastMonth"] = monthlyReport.StudentLastMonth;
            this.ViewData["lecturerThisMonth"] = monthlyReport.LecturerThisMonth;
            this.ViewData["lecturerLastMonth"] = monthlyReport.LecturerLastMonth;

            this.ViewData["roleSearch"] = listRole;

            var (users, total) = this.UserService.GetUsersStatusAndRoleWithCount(pageIndex, pageSize, searchName, searchStatus, searchRole);
            this.ViewData["users"] = users;
            this.ViewData["total"] = total;
            return View(RoutersAdmin.UserGetUserList.Page);
        }


    }
}
