using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.AuthModule.DTO;
using System.Threading.Tasks;
using FluentValidation.Results;
using FPTBlog.UserModule.Interface;
using FPTBlog.Utils.Locale;
using FPTBlog.UserModule.Entity;
using FPTBlog.AuthModule.Interface;
using FPTBlog.Utils;
using Microsoft.AspNetCore.Http;
using FPTBlog.Utils.Interface;
using FPTBlog.AuthModule;
using FPTBlog.UserModule.DTO;

namespace FPTBlog.UserModule
{
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserController : Controller
    {
        private readonly IUserService UserService;
        public UserController(IUserService UserService)
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

        [HttpPost("update")]
        public IActionResult UpdateUserHandler(string name, string email, string address, string phone, string avartarUrl)
        {
            var input = new UpdateUserDto()
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address
            };

            var isUpdate = this.UserService.UpdateUserHandler(input, this.ViewData);
            if (!isUpdate)
            {
                return View(Routers.UpdateUser.Page);
            }
            return Redirect(Routers.User.Link);
        }
    }
}