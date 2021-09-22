using System;
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
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserMvcController : Controller
    {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public UserMvcController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet("")]
        public IActionResult GetUser()
        {

            return View(Routers.User.Page);
        }


        [HttpGet("update")]
        public IActionResult UpdateUser()
        {
            return View(Routers.UpdateUser.Page);
        }


        [HttpGet("change-password")]
        public IActionResult ChangePassPage()
        {
            return View(Routers.ChangePass.Page);
        }


    }
}