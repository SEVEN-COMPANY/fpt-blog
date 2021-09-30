using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.Src.CommonModule {

    [Route("")]
    public class CommonControllerMvc : Controller {

        [HttpGet("")]
        public IActionResult GetHomePage() {

            return View(Routers.CommonGetHome.Page);
        }
    }
}
