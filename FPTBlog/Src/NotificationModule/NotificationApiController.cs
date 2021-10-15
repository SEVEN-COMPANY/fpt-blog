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

        [HttpPost("")]
        public ObjectResult HandleAddNotification([FromBody] AddNotificationDTO body) {
            var res = new ServerApiResponse<Notification>();
            ValidationResult result = new AddNotificationDTOValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isNotExistReceiver = this.UserService.GetUserByUserId(body.ReceiverId);
            if (isNotExistReceiver == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "receiverId");
                return new NotFoundObjectResult(res.getResponse());
            }

            User sender = (User) this.ViewData["user"];
            var notification = new Notification();
            notification.Content = body.Content;
            notification.SenderId = sender.UserId;
            notification.ReceiverId = body.ReceiverId;
            notification.Level = body.Level;

            this.NotificationService.AddNotification(notification);

            res.data = notification;
            return new ObjectResult(res.getResponse());
        }
    }
}