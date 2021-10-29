using Microsoft.AspNetCore.Mvc;

using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;

using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;

using FluentValidation.Results;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.UserModule {
    [Route("api/admin/user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class AdminApiController : Controller {
        private readonly IAuthService AuthService;
        private readonly INotificationService NotificationService;

        private readonly IUserService UserService;
        public AdminApiController(IUserService userService, IAuthService authService, INotificationService notificationService) {
            this.UserService = userService;
            this.AuthService = authService;
            this.NotificationService = notificationService;
        }

        [HttpPut("status")]
        public IActionResult ToggleUserStatusHandler([FromBody] ToggleUserDto body) {
            var res = new ServerApiResponse<string>();

            ValidationResult result = new ToggleUserDtoValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }
            User user = this.UserService.GetUserByUserId(body.UserId);
            if (user.Role == UserRole.LECTURER) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }
            this.UserService.ToggleUserStatusAdminHandler(user);

            User sender = (User) this.ViewData["user"];
            var notification = new Notification();
            notification.Content = body.Content;
            notification.Description = body.Description;
            notification.SenderId = sender.UserId;
            notification.ReceiverId = body.UserId;
            if (user.Status == UserStatus.ENABLE) {
                notification.Level = NotificationLevel.INFORMATION;
            }
            else {
                notification.Level = body.Level;
            }

            this.NotificationService.AddNotification(notification);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("role")]
        public IActionResult ToggleUserRoleHandler([FromBody] ToggleUserDto body) {
            var res = new ServerApiResponse<string>();

            ValidationResult result = new ToggleUserDtoValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }
            User user = this.UserService.GetUserByUserId(body.UserId);
            if (user.Role == UserRole.LECTURER) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }
            this.UserService.ToggleUserRoleAdminHandler(user);

            User sender = (User) this.ViewData["user"];
            var notification = new Notification();
            notification.Content = body.Content;
            notification.Description = body.Description;
            notification.SenderId = sender.UserId;
            notification.ReceiverId = body.UserId;
            notification.Level = NotificationLevel.INFORMATION;

            this.NotificationService.AddNotification(notification);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("chart")]
        public IActionResult GetChart() {
            var res = new ServerApiResponse<dynamic>();

            var (totalStudent, totalLecturer) = this.UserService.GetUserChartInformation();


            res.data = new {
                totalStudent = totalStudent,
                totalLecturer = totalLecturer
            };

            return new ObjectResult(res.getResponse());
        }
    }
}
