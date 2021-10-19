using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using FPTBlog.Src.AuthModule;

using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule.Entity;

using FPTBlog.Src.CategoryModule.Interface;

using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Common;

namespace FPTBlog.Src.CommonModule {

    [Route("")]
    [ServiceFilter(typeof(UserFilter))]
    public class CommonControllerMvc : Controller {

        private readonly IUploadFileService UploadFileService;
        private readonly IPostService PostService;
        private readonly ICategoryService CategoryService;
        public CommonControllerMvc(IUploadFileService uploadFileService, IPostService postService, ICategoryService categoryService) {
            this.UploadFileService = uploadFileService;
            this.PostService = postService;
            this.CategoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult GetPostForHome() {
            var (listNewest, _) = this.PostService.GetNewestPosts(4);
            var (listPopular, _) = this.PostService.GetPopularPosts(8);
            var (listHighView, _) = this.PostService.GetHighestPointPosts(4);

            List<PostViewModel> listTop4 = new List<PostViewModel>();
            foreach (var item in listNewest) {
                listTop4.Add(new PostViewModel() {
                    Post = item,
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                });
            }

            List<PostViewModel> listMiddle8 = new List<PostViewModel>();
            foreach (var item in listPopular) {
                listMiddle8.Add(new PostViewModel() {
                    Post = item,
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                });
            }

            List<PostViewModel> listBottom4 = new List<PostViewModel>();
            foreach (var item in listHighView) {
                listBottom4.Add(new PostViewModel() {
                    Post = item,
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                });
            }

            this.ViewData["top1"] = listTop4[0];
            this.ViewData["top3"] = listTop4.GetRange(1, 3);
            this.ViewData["middle4"] = listMiddle8.GetRange(0, 4);
            this.ViewData["bottom4"] = listMiddle8.GetRange(3, 4);
            this.ViewData["bottom1"] = listBottom4[0];
            this.ViewData["bottom3"] = listBottom4.GetRange(1, 3);
            return View(Routers.CommonGetHome.Page);
        }
    }
}
