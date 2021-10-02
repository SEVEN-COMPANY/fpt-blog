using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.AuthModule;


namespace FPTBlog.Src.CommonModule {

    [Route("")]
    [ServiceFilter(typeof(UserFilter))]
    public class CommonControllerMvc : Controller {

        [HttpGet("")]
        public IActionResult GetHomePage() {

            return View(Routers.CommonGetHome.Page);
        }
    }
}
