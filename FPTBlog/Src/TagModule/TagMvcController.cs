using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;


namespace FPTBlog.Src.TagModule {
    [Route("admin/tag")]
    public class TagMvcController : Controller {
        private readonly ITagService TagService;
        public TagMvcController(ITagService tagService) {
            this.TagService = tagService;
        }

        [HttpGet("add")]
        public IActionResult AddTagPage() {
            return View(RoutersAdmin.AddTag.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateTagPage(string tagId) {
            Tag tag = this.TagService.GetTagByTagId(tagId);
            if (tag == null) {
                ServerMvcResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return View(RoutersAdmin.UpdateTag.Page);
            }
            ViewData["tag"] = tag;
            return View(RoutersAdmin.UpdateTag.Page);
        }

        [HttpGet("")]
        public IActionResult GetTagsPage(string searchName, TagStatus searchStatus = TagStatus.ACTIVE, int pageSize = 12, int pageIndex = 0) {
            if (searchName == null) {
                searchName = "";
            }

            var (listTag, total) = this.TagService.GetTagsWithCount(pageIndex, pageSize, searchName, searchStatus);
            ViewData["tags"] = listTag;
            ViewData["total"] = total;

            return View(RoutersAdmin.GetTags.Page);
        }
    }
}
