using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Utils.Locale;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FluentValidation.Results;
using System.Collections.Generic;

namespace FPTBlog.Src.UserModule {
    [Route("/api/user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserApiController : Controller {


        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public UserApiController(IUserService userService, IAuthService authService) {
            this.UserService = userService;
            this.AuthService = authService;
        }

        [HttpGet("")]
        public IActionResult GetUser() {
            var res = new ServerApiResponse<User>();
            var user = (User) this.ViewData["user"];
            user.Password = "";

            res.data = user;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        public IActionResult UpdateUserHandler([FromBody] UpdateUserDto input) {
            var res = new ServerApiResponse<User>();


            User currentUser = (User) this.ViewData["user"];

            ValidationResult result = new UpdateUserDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = this.UserService.GetUserByUserId(currentUser.UserId);
            user.Name = input.Name;
            user.Email = input.Email;
            user.Phone = input.Phone;
            user.Address = input.Address;
            this.UserService.UpdateUser(user);
            res.data = user;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("change-password")]
        public IActionResult ChangePasswordHandler([FromBody] ChangePassDto body) {


            var res = new ServerApiResponse<string>();
            User user = (User) this.ViewData["user"];

            ValidationResult result = new ChangePassDtoValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            if (user.GoogleId != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isCorrectPassword = this.AuthService.ComparePassword(body.OldPassword, user.Password);
            if (!isCorrectPassword) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_OLD_PASSWORD_IS_WRONG);
                return new BadRequestObjectResult(res.getResponse());
            }
            user.Password = this.AuthService.HashingPassword(body.NewPassword);
            this.UserService.UpdateUser(user);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("search")]
        public IActionResult GetUsersByPage(int pageSize, int pageIndex, string search) {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerApiResponse<IDictionary<string, object>> res = new ServerApiResponse<IDictionary<string, object>>();
            if (search == null) {
                search = "";
            }
            var (users, total) = this.UserService.GetUsersWithCount(pageSize, pageIndex, search);
            dataRes.Add("blogs", users);
            dataRes.Add("total", total);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

    }

}
