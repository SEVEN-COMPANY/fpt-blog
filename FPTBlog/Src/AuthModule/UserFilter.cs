
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using FPTBlog.Utils.Common;

namespace FPTBlog.Src.AuthModule {
    public class UserFilter : IActionFilter {
        private readonly IJwtService JWTService;
        private readonly IUserRepository UserRepository;
        private readonly IUserService UserService;
        public UserFilter(IJwtService jwtService, IUserRepository userRepository, IUserService userService) {

            this.JWTService = jwtService;
            this.UserRepository = userRepository;
            this.UserService = userService;
        }

        public void OnActionExecuted(ActionExecutedContext context) {
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            bool isValid = this.GuardHandler(context);
        }

        public bool GuardHandler(ActionExecutingContext context) {

            try {
                var cookies = new Dictionary<string, string>();
                var values = ((string) context.HttpContext.Request.Headers["Cookie"]).Split(',', ';');


                foreach (var parts in values) {
                    var cookieArray = parts.Trim().Split('=');
                    if (cookieArray.Length >= 2) {
                        cookies.Add(cookieArray[0], cookieArray[1]);
                    }
                }

                if (!cookies.TryGetValue("auth-token", out _)) {
                    return false;
                }
                var token = this.JWTService.VerifyToken(cookies["auth-token"]).Split(";");

                if (token[0] == null) {
                    return false;
                }
                var user = this.UserService.GetUserByUserId(token[0]);
                if (user == null) {
                    return false;
                }

                Controller controller = context.Controller as Controller;
                controller.ViewData["user"] = user;
                return true;

            }
            catch (Exception error) {
                Console.WriteLine(error);
                return false;
            }
        }
    }
}
