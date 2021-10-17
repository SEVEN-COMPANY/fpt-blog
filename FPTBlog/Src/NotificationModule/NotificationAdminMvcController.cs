using FPTBlog.Src.AuthModule;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.NotificationModule {
    [Route("admin/notification")]
    [ServiceFilter(typeof(AuthGuard))]
    public class NotificationAdminMvcController : Controller {

        private readonly INotificationService NotificationService;
        public NotificationAdminMvcController(INotificationService notificationService) {
            this.NotificationService = notificationService;
        }

        [HttpGet("")]
        public IActionResult GetNotifications() {

            return View(RoutersAdmin.NotificationList.Page);

        }
    }
}
