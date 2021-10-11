using System;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.CategoryModule.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FPTBlog.Src.UserModule {
    [Route("user")]
    [ServiceFilter(typeof(AuthGuard))]
    public class UserMvcController : Controller {
        private readonly IPostService PostService;
        private readonly IUserService UserService;
        private readonly ICategoryService CategoryService;
        public UserMvcController(IUserService UserService, IPostService PostService, ICategoryService CategoryService) {
            this.UserService = UserService;
            this.PostService = PostService;
            this.CategoryService = CategoryService;
        }

        [HttpGet("")]
        public IActionResult GetUser() {

            return View(Routers.UserGetProfile.Page);
        }

        // follower lay nhung thang dang follow minh
        [HttpGet("follower")]
        public IActionResult GetFollower(string userId, int pageSize = 12, int pageIndex = 0, string searchName = "") {
            var (list, countFollower) = this.UserService.GetFollowerForPage(userId, pageIndex, pageSize, searchName);
            var (_, countFollowing) = this.UserService.CalculateFollowing(userId);
            var (_, countPost) = this.PostService.GetPostsForProfile(userId, 1, 0, "", "", PostStatus.APPROVED);

            this.ViewData["users"] = list;
            this.ViewData["countFollower"] = countFollower;
            this.ViewData["countFollowing"] = countFollowing;
            this.ViewData["countPost"] = countPost;

            return View(Routers.UserGetFollower.Page);
        }

        // following la nhung thang minh follow no
        [HttpGet("following")]
        public IActionResult GetFollowing(string userId, int pageSize = 12, int pageIndex = 0, string searchName = "") {
            var (list, countFollowing) = this.UserService.GetFollowingForPage(userId, pageIndex, pageSize, searchName);
            var (_, countFollower) = this.UserService.GetFollowerForPage(userId, pageIndex, pageSize, searchName);
            var (_, countPost) = this.PostService.GetPostsForProfile(userId, 1, 0, "", "", PostStatus.APPROVED);
            this.ViewData["users"] = list;
            this.ViewData["countFollower"] = countFollower;
            this.ViewData["countFollowing"] = countFollowing;
            this.ViewData["countPost"] = countPost;

            return View(Routers.UserGetFollower.Page);
        }

        [HttpGet("me")]
        public IActionResult GetProfile(int pageSize = 12, int pageIndex = 0, string searchTitle = "", string searchCategoryId = "") {
            var user = (User) this.ViewData["user"];

            if (searchTitle == null) {
                searchTitle = "";
            }
            if (searchCategoryId == null) {
                searchCategoryId = "";
            }

            var categoryDropList = this.CategoryService.GetCategoryDropList();
            categoryDropList.Add(new SelectListItem() { Value = "", Text = "All" });
            this.ViewData["categories"] = new SelectList(categoryDropList);

            var (_, countFollower) = this.UserService.CalculateFollower(user.UserId);
            var (_, countFollowing) = this.UserService.CalculateFollowing(user.UserId);

            var (posts, countPost) = this.PostService.GetPostsForProfile(user.UserId, pageSize, pageIndex, searchTitle, searchCategoryId, PostStatus.APPROVED);
            List<PostViewModel> listBlogs = new List<PostViewModel>();
            foreach (var item in posts) {
                PostViewModel pvm = new PostViewModel() {
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                };

                pvm.Post = item;
                listBlogs.Add(pvm);
            }

            this.ViewData["user"] = user;
            this.ViewData["countFollower"] = countFollower;
            this.ViewData["countFollowing"] = countFollowing;
            this.ViewData["countPost"] = countPost;
            this.ViewData["posts"] = listBlogs;

            return View(Routers.UserGetMyProfile.Page);
        }

        [HttpGet("profile")]
        public IActionResult GetProfile(string userId, string searchTitle = "", string searchCategoryId = "", int pageSize = 12, int pageIndex = 0) {
            User user = this.UserService.GetUserByUserId(userId);
            if (user == null) {
                return NotFound();
            }

            if (searchTitle == null) {
                searchTitle = "";
            }
            if (searchCategoryId == null) {
                searchCategoryId = "";
            }
            var categoryDropList = this.CategoryService.GetCategoryDropList();
            categoryDropList.Add(new SelectListItem() { Value = "", Text = "All" });
            this.ViewData["categories"] = new SelectList(categoryDropList);


            var (_, countFollower) = this.UserService.CalculateFollower(user.UserId);
            var (_, countFollowing) = this.UserService.CalculateFollowing(user.UserId);

            var (posts, countPost) = this.PostService.GetPostsForProfile(user.UserId, pageSize, pageIndex, searchTitle, searchCategoryId, PostStatus.APPROVED);
            List<PostViewModel> listBlogs = new List<PostViewModel>();
            foreach (var item in posts) {
                PostViewModel pvm = new PostViewModel() {
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                };

                pvm.Post = item;
                listBlogs.Add(pvm);
            }

            this.ViewData["user"] = user;
            this.ViewData["countFollower"] = countFollower;
            this.ViewData["countFollowing"] = countFollowing;
            this.ViewData["countPost"] = countPost;
            this.ViewData["posts"] = listBlogs;

            return View(Routers.UserGetProfile.Page);
        }

        [HttpGet("me/save")]
        public IActionResult GetSavePost(int pageSize = 12, int pageIndex = 0, string searchTitle = "", string searchCategoryId = "") {
            var user = (User) this.ViewData["user"];

            if (searchTitle == null) {
                searchTitle = "";
            }
            if (searchCategoryId == null) {
                searchCategoryId = "";
            }

            var (posts, count) = this.UserService.GetSavePost(user.UserId, pageIndex, pageSize, searchTitle, searchCategoryId);
            List<PostViewModel> listPosts = new List<PostViewModel>();
            foreach (var item in posts) {
                PostViewModel pvm = new PostViewModel() {
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                };
                pvm.Post = item;
                listPosts.Add(pvm);
            }

            return Json(new {
                listPosts = listPosts,
                count = count
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
