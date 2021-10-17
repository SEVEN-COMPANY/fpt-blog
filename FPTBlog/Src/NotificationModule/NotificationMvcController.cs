using FPTBlog.Src.AuthModule;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;

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

            this.ViewData["notifications"] = notifications;
            this.ViewData["totalNotifications"] = total;

            return View(Routers.NotificationMe.Page);
        }

    }
}
