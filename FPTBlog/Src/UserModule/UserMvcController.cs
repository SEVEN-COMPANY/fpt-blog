using System;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.UserModule {
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserMvcController : Controller {
        private readonly IPostService PostService;
        private readonly IUserService UserService;
        public UserMvcController(IUserService UserService, IPostService PostService) {
            this.UserService = UserService;
            this.PostService = PostService;
        }

        [HttpGet("")]
        public IActionResult GetUser() {

            return View(Routers.UserGetProfile.Page);
        }

        [HttpGet("me")]
        public IActionResult GetProfile(int pageSize = 12, int pageIndex = 0, string searchTitle = "", string searCategoryId = "", PostStatus status = PostStatus.APPROVED) {
            var user = (User) this.ViewData["user"];

            var (listFollower, countFollower) = this.UserService.CalculateFollower(user.UserId);
            var (listFollowing, countFollowing) = this.UserService.CalculateFollowing(user.UserId);

            var (posts, count) = this.PostService.GetPostsForProfile(pageSize, pageIndex, searchTitle, searCategoryId, status);

            this.ViewData["profile"] = user;
            // return Json(new {
            //     user = user,
            //     listFollower = listFollower,
            //     countFollower = countFollower,
            //     listFollowing = listFollowing,
            //     countFollowing = countFollowing,
            //     posts = posts,
            //     count = count
            // });
            return View(Routers.UserGetMyProfile.Page);
        }

        [HttpGet("profile")]
        public IActionResult GetProfile(string userId, int pageSize, int pageIndex, string searchTitle, string searCategoryId, PostStatus status) {
            User user = this.UserService.GetUserByUserId(userId);
            if (user == null) {
                return NotFound();
            }

            var (listFollower, countFollower) = this.UserService.CalculateFollower(user.UserId);
            var (listFollowing, countFollowing) = this.UserService.CalculateFollowing(user.UserId);

            this.ViewData["profile"] = user;
            return Json(new {
                user = user,
                listFollower = listFollower,
                countFollower = countFollower,
                listFollowing = listFollowing,
                countFollowing = countFollowing
            });
        }

        [HttpGet("update")]
        public IActionResult UpdateUser() {
            return View(Routers.UserPutUser.Page);
        }


        [HttpGet("change-password")]
        public IActionResult ChangePassPage() {
            return View(Routers.UserPutPassword.Page);
        }


    }
}
