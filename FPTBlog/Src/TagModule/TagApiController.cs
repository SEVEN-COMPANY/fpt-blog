using FluentValidation.Results;
using FPTBlog.Src.TagModule.DTO;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.TagModule {

    [ApiController]
    [Route("/api/tag")]
    public class TagApiController : Controller {
        private readonly ITagService TagService;
        public TagApiController(ITagService tagService) {
            this.TagService = tagService;
        }

        [HttpPost("")]
        public ObjectResult AddTagHandler([FromBody] AddTagDto input) {
            var res = new ServerApiResponse<Tag>();

            ValidationResult result = new AddTagDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var existedTag = this.TagService.GetTagByName(input.Name);
            if (existedTag != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "name");
                return new BadRequestObjectResult(res.getResponse());
            }

            Tag tag = new Tag();
            tag.Name = input.Name;
            this.TagService.SaveTag(tag);

            res.data = tag;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpGet("all")]
        public ObjectResult GetQualityBlogOfTagHandler() {
            var res = new ServerApiResponse<object>();
            var listTag = this.TagService.GetTagsWithCount();
            res.data = listTag;

            return new ObjectResult(res.getResponse());
        }
        [HttpGet("")]
        public ObjectResult GetTagByName(string name = "") {
            var res = new ServerApiResponse<object>();
            var listTag = this.TagService.GetTagsByName(name);
            res.data = listTag;

            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        public IActionResult UpdateTagHandler([FromBody] UpdateTagDto input) {
            var res = new ServerApiResponse<Tag>();

            var tag = this.TagService.GetTagByTagId(input.TagId);
            if (tag == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "tagId");
                return new BadRequestObjectResult(res.getResponse());
            }
            this.ViewData["tag"] = tag;

            ValidationResult result = new UpdateTagDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            tag.Name = input.Name;
            tag.Status = input.Status;
            this.TagService.UpdateTag(tag);

            res.data = tag;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}
