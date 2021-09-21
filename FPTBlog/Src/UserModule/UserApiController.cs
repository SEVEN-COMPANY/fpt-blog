using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.UserModule.DTO;
using FPTBlog.Utils.Locale;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FluentValidation.Results;


namespace FPTBlog.Src.UserModule
{
    [Route("/api/user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserApiController : Controller
    {


        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public UserApiController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet("")]
        public IActionResult GetUser()
        {
            var res = new ServerApiResponse<User>();
            var user = (User)this.ViewData["user"];
            user.Password = "";

            res.data = user;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        public IActionResult UpdateUserHandler([FromBody] UpdateUserDto input)
        {
            var res = new ServerApiResponse<User>();


            User currentUser = (User)this.ViewData["user"];

            ValidationResult result = new UpdateUserDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = this.UserService.GetUserByUserId(currentUser.UserId);
            user.Name = input.Name;
            user.Email = input.Name;
            user.Phone = input.Phone;
            user.Address = input.Address;
            this.UserService.UpdateUser(user);
            res.data = user;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }


    }

}