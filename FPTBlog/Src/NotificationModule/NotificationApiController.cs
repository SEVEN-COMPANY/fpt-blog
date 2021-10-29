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
    public class NotificationApiController : Controller {
        private readonly INotificationService NotificationService;
        private readonly IUserService UserService;

        public NotificationApiController(IUserService userService, INotificationService notificationService) {
            this.NotificationService = notificationService;
            this.UserService = userService;
        }

        [ServiceFilter(typeof(AuthGuard))]
        [HttpGet("")]
        public ObjectResult GetNotificationHandler(string notificationId) {
            var res = new ServerApiResponse<Notification>();
            var notification = this.NotificationService.GetNotificationByNotificationId(notificationId);

            res.data = notification;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("")]
        public ObjectResult HandleAddNotification([FromBody] AddNotificationUserDTO body) {
            var res = new ServerApiResponse<Notification>();
            ValidationResult result = new AddNotificationUserDTOValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var (users, _) = this.UserService.GetUsersStatusAndRoleWithCount(0, 1, "", UserStatus.ENABLE, UserRole.LECTURER);

            User sender = this.UserService.GetUserByUsername(body.Username);
            var notification = new Notification();
            notification.Content = "This user requests to unlock their account";
            notification.Description = body.Description;
            notification.SenderId = sender.UserId;
            notification.ReceiverId = users[0].UserId;
            notification.Level = NotificationLevel.INFORMATION;

            this.NotificationService.AddNotification(notification);
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_BLOCK_USER);
            res.data = notification;
            return new ObjectResult(res.getResponse());
        }
    }
}
