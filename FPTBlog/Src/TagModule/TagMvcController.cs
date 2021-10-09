using FPTBlog.Src.AuthModule;
using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBlog.Src.TagModule {
    [Route("admin/tag")]
    [ServiceFilter(typeof(AuthGuard))]
    public class TagMvcController : Controller {
        private readonly ITagService TagService;
        public TagMvcController(ITagService tagService) {
            this.TagService = tagService;
        }



        [HttpGet("")]
        public IActionResult GetTagsPage(string searchName, TagStatus searchStatus = TagStatus.ACTIVE, int pageSize = 12, int pageIndex = 0) {
            if (searchName == null) {
                searchName = "";
            }

            var statusList = this.TagService.GetTagStatusDroplist();
            statusList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList list = new SelectList(statusList, "");

            this.ViewData["statusSearch"] = list;

            var (createdTagLastMonth, createdTagThisMonth) = this.TagService.GetCreatedTag();


            var (listTag, total) = this.TagService.GetTagsBelongToPostWithCount(pageIndex, pageSize, searchName, searchStatus);
            var (hotTrendingTagName, hotTrendingTagCount) = this.TagService.GetHotTrendingTag();

            ViewData["tags"] = listTag;
            ViewData["total"] = total;
            ViewData["hotTrendingTagName"] = hotTrendingTagName;
            ViewData["hotTrendingTagCount"] = hotTrendingTagCount;
            ViewData["numOfTagsCreatedCurrentMonth"] = createdTagThisMonth;
            ViewData["numOfTagsCreatedLastMonth"] = createdTagLastMonth;

            return View(RoutersAdmin.TagGetTagList.Page);
        }
    }
}
