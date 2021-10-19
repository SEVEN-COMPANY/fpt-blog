using FPTBlog.Utils.Common;
using FPTBlog.Src.AuthModule;

using Microsoft.AspNetCore.Mvc;


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
