
using System;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.DTO;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.AuthModule;
using FPTBlog.Utils.Common;
using FPTBlog.Src.BlogModule.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FPTBlog.Src.CategoryModule {
    [Route("/admin/category")]
    [ServiceFilter(typeof(AuthGuard))]
    public class CategoryMvcController : Controller {
        private readonly ICategoryService CategoryService;
        private readonly IBlogService BlogService;


        public CategoryMvcController(ICategoryService categoryService, IBlogService blogService) {
            this.BlogService = blogService;
            this.CategoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult Category(string searchName, CategoryStatus searchStatus = CategoryStatus.ACTIVE, int pageSize = 12, int indexPage = 0) {
            if (searchName == null) {
                searchName = "";
            }

            var (categories, total) = this.CategoryService.GetCategories(indexPage, pageSize, searchName, searchStatus);

            this.ViewData["categories"] = categories;
            this.ViewData["total"] = total;
            return View(RoutersAdmin.Category.Page);
        }

        [HttpGet("create")]
        public IActionResult AddCategoryPage() {
            SelectList list = new SelectList(this.CategoryService.GetRadioStatusList(), "1");
            this.ViewData["status"] = list;

            return View(RoutersAdmin.CreateCategory.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateCategory(string categoryId) {
            var category = this.CategoryService.GetCategoryByCategoryId(categoryId);
            SelectList list = new SelectList(this.CategoryService.GetRadioStatusList(), "1");
            this.ViewData["status"] = list;
            this.ViewData["category"] = category;
            return View(RoutersAdmin.UpdateCategory.Page);
        }
    }
}
