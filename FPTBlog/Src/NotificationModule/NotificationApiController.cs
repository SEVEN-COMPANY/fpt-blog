using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.NotificationModule.DTO;
using FPTBlog.Src.NotificationModule.Entity;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.NotificationModule {
    [Route("/api/notification")]
    [ServiceFilter(typeof(AuthGuard))]
    public class NotificationApiController : Controller {
        private readonly INotificationService NotificationService;
        private readonly IUserService UserService;

        public NotificationApiController(IUserService userService, INotificationService notificationService) {
            this.NotificationService = notificationService;
            this.UserService = userService;
        }

        [HttpGet("")]
        public ObjectResult GetNotificationHandler(string notificationId) {
            var res = new ServerApiResponse<Notification>();
            var notification = this.NotificationService.GetNotificationByNotificationId(notificationId);

            res.data = notification;
            return new ObjectResult(res.getResponse());
        }
    }
}