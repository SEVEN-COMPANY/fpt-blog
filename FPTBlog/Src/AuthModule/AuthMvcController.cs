﻿using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FPTBlog.Src.AuthModule.DTO;
using System.Threading.Tasks;
using FluentValidation.Results;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils.Locale;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Utils;
using Microsoft.AspNetCore.Http;
using FPTBlog.Utils.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace FPTBlog.Src.AuthModule
{

    [Route("/auth")]
    public class AuthMvcController : Controller
    {


        private readonly IAuthService AuthService;
        private readonly IUserService UserService;
        private readonly IJwtService JwtService;
        public AuthMvcController(IAuthService authService, IJwtService jwtService, IUserService userService)
        {
            this.AuthService = authService;
            this.UserService = userService;
            this.JwtService = jwtService;
        }


        [HttpGet("login")]
        public IActionResult LoginPage()
        {
            return View(Routers.Login.Page);
        }

        [HttpGet("google")]
        public IActionResult LoginGoogle([FromQuery(Name = "credential")] string credential)
        {
            JwtSecurityToken jwtToken = this.JwtService.Decode(credential);
            string id = (string)this.JwtService.GetDataFromJwtToken(jwtToken, "sub");
            User user = this.UserService.GetUserByGoogleId(id);
            if (user == null)
            {
                user = new User();
                user.UserId = Guid.NewGuid().ToString();
                user.GoogleId = (string)this.JwtService.GetDataFromJwtToken(jwtToken, "sub");
                user.Name = (string)this.JwtService.GetDataFromJwtToken(jwtToken, "name");
                user.Email = (string)this.JwtService.GetDataFromJwtToken(jwtToken, "email");
                user.AvatarUrl = (string)this.JwtService.GetDataFromJwtToken(jwtToken, "picture");
                user.Role = UserRole.STUDENT;
                this.UserService.SaveUser(user);
            }

            var token = this.JwtService.GenerateToken(user.UserId);
            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Redirect(Routers.Home.Link);

        }



        [HttpGet("register")]
        public IActionResult RegisterPage()
        {
            return View(Routers.Register.Page);
        }
    }
}