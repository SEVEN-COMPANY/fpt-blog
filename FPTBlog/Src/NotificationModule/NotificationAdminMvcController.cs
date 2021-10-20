using System;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.NotificationModule.Entity;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.NotificationModule {
    [Route("admin/notification")]
    [ServiceFilter(typeof(AuthGuard))]
    public class NotificationAdminMvcController : Controller {

        private readonly INotificationService NotificationService;
        public NotificationAdminMvcController(INotificationService notificationService) {
            this.NotificationService = notificationService;
        }

        [HttpGet("")]
        public IActionResult GetNotifications(string search, NotificationLevel searchLevel, string startDate, string endDate, int pageSize = 12, int pageIndex = 0) {
            if (search == null) {
                search = "";
            }

            var now = DateTime.Now;
            if (endDate == null) {
                endDate = now.AddYears(5).ToString("yyyy-MM-dd");
            }
            if (startDate == null) {
                startDate = now.AddYears(-5).ToString("yyyy-MM-dd");
            }

            this.ViewData["level"] = new SelectList(this.NotificationService.GetNotificationLevelDropList(), NotificationLevel.INFORMATION);
            // get status search list for search by status
            var levelList = this.NotificationService.GetNotificationLevelDropList();
            levelList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList listLevel = new SelectList(levelList, "");
            this.ViewData["levelSearch"] = listLevel;

            var (notifications, total) = this.NotificationService.GetNotificationsLevelAndTimeWithCount(pageIndex, pageSize, search, searchLevel, startDate, endDate);
            this.ViewData["notifications"] = notifications;
            this.ViewData["total"] = total;
            return View(RoutersAdmin.NotificationList.Page);
        }
    }
}
