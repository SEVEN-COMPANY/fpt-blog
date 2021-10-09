using System;
using System.Collections.Generic;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.RewardModule.DTO;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.RewardModule {
    [Route("admin/reward")]
    [ServiceFilter(typeof(AuthGuard))]
    public class RewardMvcController : Controller {

        [Route("")]
        public IActionResult GetAllBlogs() {

            return View(RoutersAdmin.RewardGetHome.Page);
        }
        [Route("badge")]
        public IActionResult GetAllBadges() {

            return View(RoutersAdmin.RewardGetBadge.Page);
        }


    }
}
