using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using System.Collections.Generic;


namespace FPTBlog.Src.UserModule
{
    [Route("/api/admin")]
    public class AdminApiController
    {
        private readonly IUserService UserService;
        public AdminApiController(IUserService userService)
        {
            this.UserService = userService;
        }

        [HttpGet("user")]
        public IActionResult GetUsersByPage(int pageSize, int page, string search)
        {
            IDictionary<string, object> dataRes = new Dictionary<string, object>();
            ServerApiResponse<IDictionary<string, object>> res = new ServerApiResponse<IDictionary<string, object>>();
            if (search == null)
            {
                search = "";
            }
            var (users, total) = this.UserService.GetUsersByPageAndCount(pageSize, page - 1, search);
            dataRes.Add("blogs", users);
            dataRes.Add("total", total);
            res.data = dataRes;
            return new ObjectResult(res.getResponse());
        }

    }
}