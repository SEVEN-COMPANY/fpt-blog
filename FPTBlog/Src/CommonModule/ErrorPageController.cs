using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.AuthModule;


namespace FPTBlog.Src.CommonModule {

    [Route("error")]
    [ServiceFilter(typeof(UserFilter))]
    public class ErrorPageController : Controller {

        [Route("404")]
        public IActionResult HandleNotFound() {


            return View(Routers.NotFoundPage.Page);
        }
        [Route("500")]
        public IActionResult HandleInternalError() {


            return View(Routers.ErrorPage.Page);
        }
    }
}
