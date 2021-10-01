using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Utils.Locale;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FluentValidation.Results;

namespace FPTBlog.Src.UserModule {
    [Route("api/admin")]
    [ServiceFilter(typeof(AuthGuard))]
    public class AdminApiController : Controller {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public AdminApiController(IUserService userService, IAuthService authService) {
            this.UserService = userService;
            this.AuthService = authService;
        }
        [HttpPost("block")]
        public IActionResult BlockUserHandler([FromBody] BlockUserDto body) {
            var res = new ServerApiResponse<BlockUserDto>();
            User admin = (User) this.ViewData["user"];
            if (admin.Role != UserRole.LECTURER) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }
            ValidationResult result = new BlockUserDtoValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }
            User blockUser = this.UserService.GetUserByUserId(body.UserIdBlock);
            if (blockUser.Role == UserRole.LECTURER) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }
            this.UserService.BlockUserByAdminHandler(blockUser);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}
