using FPTBlog.Src.AuthModule;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.NotificationModule {
    [Route("notification")]
    [ServiceFilter(typeof(AuthGuard))]
    public class NotificationMvcController : Controller {
        private readonly INotificationService NotificationService;

        public NotificationMvcController(INotificationService notificationService) {
            this.NotificationService = notificationService;
        }

        [HttpGet("me")]
        public IActionResult GetNotifications() {
            User user = (User) this.ViewData["user"];
            var (notifications, total) = this.NotificationService.GetUserNotification(user.UserId);
            var levelList = this.NotificationService.GetNotificationLevelDropList();
            levelList.Insert(0, new SelectListItem() { Text = "All", Value = "" });
            SelectList listLevel = new SelectList(levelList, "");

            this.ViewData["levelSearch"] = listLevel;
            this.ViewData["notifications"] = notifications;
            this.ViewData["total"] = total;

            return View(Routers.NotificationMe.Page);
        }

    }
}
