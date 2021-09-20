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
    }

}