using System.Collections.Generic;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.PostModule.DTO;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.TagModule.Interface;
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

        [HttpPost("")]
        public IActionResult ApprovedHandler([FromBody] ApprovedPostDto input) {
            var res = new ServerApiResponse<(Post, string)>();

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

            post.Status = input.Status;

            res.data = (post, input.Note);
            return new ObjectResult(res.getResponse());
        }

    }
}
