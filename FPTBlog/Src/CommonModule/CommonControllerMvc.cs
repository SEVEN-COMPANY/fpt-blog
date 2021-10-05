using System;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.AuthModule;
using FPTBlog.Utils.Interface;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.PostModule.Entity;
using System.Collections.Generic;

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
            // var (listHighestPoint, _) = this.PostService.GetHighestPointPosts(4);
            // var (listPopular, _) = this.PostService.GetPopularPosts(4);
            // var (listNewest, _) = this.PostService.GetNewestPosts(4);

            // PostViewModel postViewModelTop1 = new PostViewModel() {
            //     Post = listHighestPoint[0],
            //     NumberOfComment = this.PostService.GetCommentOfPost(listHighestPoint[0]).Item2
            // };

            // List<PostViewModel> listTop3 = new List<PostViewModel>();
            // for (int i = 1; i <= 3; i++) {
            //     PostViewModel pvm = new PostViewModel() {
            //         Post = listHighestPoint[i],
            //         NumberOfComment = this.PostService.GetCommentOfPost(listHighestPoint[i]).Item2
            //     };
            //     listTop3.Add(pvm);
            // }

            // List<PostViewModel> listMiddle4 = new List<PostViewModel>();
            // foreach (var item in listPopular) {
            //     listMiddle4.Add(new PostViewModel() {
            //         Post = item,
            //         NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
            //     });
            // }

            // List<PostViewModel> listBottom4 = new List<PostViewModel>();
            // foreach (var item in listNewest) {
            //     listBottom4.Add(new PostViewModel() {
            //         Post = item,
            //         NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
            //     });
            // }

            // this.ViewData["top1"] = postViewModelTop1;
            // this.ViewData["top3"] = listTop3;
            // this.ViewData["middle4"] = listMiddle4;
            // this.ViewData["bottom4"] = listBottom4;


            var (listPopular, _) = this.PostService.GetPopularPosts(16);


            List<PostViewModel> listMiddle4 = new List<PostViewModel>();
            foreach (var item in listPopular) {
                listMiddle4.Add(new PostViewModel() {
                    Post = item,
                    NumberOfComment = this.PostService.GetCommentOfPost(item).Item2
                });
            }




            this.ViewData["top1"] = listMiddle4.GetRange(0, 1)[0];
            this.ViewData["top3"] = listMiddle4.GetRange(1, 3);
            this.ViewData["middle4"] = listMiddle4.GetRange(3, 4);
            this.ViewData["bottom4"] = listMiddle4.GetRange(8, 4);
            this.ViewData["bottom1"] = listMiddle4.GetRange(12, 1)[0];
            this.ViewData["bottom3"] = listMiddle4.GetRange(13, 3);
            return View(Routers.CommonGetHome.Page);
        }
    }
}
