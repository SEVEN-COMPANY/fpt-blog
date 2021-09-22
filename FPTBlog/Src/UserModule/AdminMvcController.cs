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

    [Route("admin")]
    public class AdminMvcController : Controller
    {
        private readonly IAuthService AuthService;

        private readonly IUserService UserService;
        public AdminMvcController(IUserService userService, IAuthService authService)
        {
            this.UserService = userService;
            this.AuthService = authService;
        }



        [HttpGet("")]
        public IActionResult GetUser()
        {

            return View(Routers.AdminDashboard.Page);
        }

    }
}