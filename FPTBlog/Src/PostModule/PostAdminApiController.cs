using System;
using FluentValidation.Results;

using FPTBlog.Src.AuthModule;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.PostModule.DTO;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Src.UserModule.Entity;

using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;

using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.PostModule {
    [Route("/api/admin/post")]
    [ServiceFilter(typeof(AuthGuard))]
    public class PostAdminApiController : Controller {
        private readonly IPostService PostService;
        private readonly ICategoryService CategoryService;
        private readonly ITagService TagService;
        public PostAdminApiController(IPostService postService, ICategoryService categoryService, ITagService tagService) {
            this.PostService = postService;
            this.CategoryService = categoryService;
            this.TagService = tagService;
        }

        [HttpPost("approved")]
        public IActionResult ApprovedHandler([FromBody] ApprovedPostDto input) {
            var user = (User) this.ViewData["user"];
            var res = new ServerApiResponse<Post>();

            ValidationResult result = new ApprovedPostDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            Post post = this.PostService.GetPostByPostId(input.PostId);
            if (post == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "postId");
                return new NotFoundObjectResult(res.getResponse());
            }

            if (post.Status != PostStatus.WAIT) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            post.Lecturer = user;
            post.LecturerId = user.UserId;
            post.Status = input.Status;
            post.Note = input.Note;
            this.PostService.UpdatePost(post);
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("chart")]
        public ObjectResult PostChart(string fromDate, string toDate) {
            var now = DateTime.Now;

            if (fromDate == null || toDate == null) {
                toDate = now.ToString("yyyy-MM-dd");
                fromDate = now.AddYears(-1).ToString("yyyy-MM-dd");
            }


            var res = new ServerApiResponse<dynamic>();
            DateTime fDate = Convert.ToDateTime(fromDate);
            DateTime tDate = Convert.ToDateTime(toDate);
            var postChart = this.PostService.GetPostChart(fDate, tDate);
            res.data = postChart;
            return new ObjectResult(res.getResponse());
        }
    }
}
