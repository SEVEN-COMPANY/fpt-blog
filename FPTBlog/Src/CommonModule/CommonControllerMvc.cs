using System;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.CommonModule {

    [Route("")]
    public class CommonControllerMvc : Controller {

        [HttpGet("")]
        public IActionResult GetHomePage() {

            return View(Routers.CommonGetHome.Page);
        }
    }
}
