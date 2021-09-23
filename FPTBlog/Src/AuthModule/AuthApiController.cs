
using FPTBlog.Utils.Common;
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
    [Route("/api/auth")]

    public class AuthApiController : Controller
    {

        private readonly IAuthService AuthService;
        private readonly IUserService UserService;
        private readonly IJwtService JwtService;
        public AuthApiController(IAuthService authService, IJwtService jwtService, IUserService userService)
        {
            this.AuthService = authService;
            this.UserService = userService;
            this.JwtService = jwtService;
        }

        [HttpPost("login")]
        public ObjectResult LoginHandler([FromBody] LoginUserDto body)
        {
            var res = new ServerApiResponse<string>();

            ValidationResult result = new LoginUserDtoValidator().Validate(body);
            if (!result.IsValid)
            {

                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = this.UserService.GetUserByUsername(body.Username);
            if (user == null)
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isCorrectPassword = this.AuthService.ComparePassword(body.Password, user.Password);
            if (!isCorrectPassword)
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_LOGIN_FAIL);
                return new BadRequestObjectResult(res.getResponse());
            }

            var token = this.JwtService.GenerateToken(user.UserId);
            this.HttpContext.Response.Cookies.Append("auth-token", token, new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(30),
                SameSite = SameSiteMode.None,
                Secure = true

            });

            return new ObjectResult(res.getResponse());
        }

        [HttpPost("register")]
        public IActionResult RegisterHandler([FromBody] RegisterUserDto body)
        {
            var res = new ServerApiResponse<string>();

            ValidationResult result = new RegisterUserDtoValidator().Validate(body);
            if (!result.IsValid)
            {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isExistUser = this.UserService.GetUserByUsername(body.Username);
            if (isExistUser != null)
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "username");
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = new User();
            user.UserId = Guid.NewGuid().ToString();
            user.Name = body.Name;
            user.Username = body.Username;
            user.CreateDate = DateTime.Now.ToShortDateString();
            user.Password = this.AuthService.HashingPassword(body.Password);
            this.UserService.SaveUser(user);

            return new ObjectResult(res.getResponse());
        }



    }
}