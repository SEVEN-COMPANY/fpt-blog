
using System;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.DTO;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.AuthModule;
using FPTBlog.Utils.Common;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FPTBlog.Src.CategoryModule {
    [Route("/admin/category")]
    [ServiceFilter(typeof(AuthGuard))]
    public class CategoryMvcController : Controller {
        private readonly ICategoryService CategoryService;


        public CategoryMvcController(ICategoryService categoryService) {
            this.CategoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult Category(string searchName, CategoryStatus searchStatus, int pageSize = 12, int pageIndex = 0) {
            if (searchName == null) {
                searchName = "";
            }

            this.ViewData["status"] = new SelectList(this.CategoryService.GetCategoryStatusDropList(), CategoryStatus.ACTIVE);

            var statusList = this.CategoryService.GetCategoryStatusDropList();
            statusList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList list = new SelectList(statusList, "");
            this.ViewData["statusSearch"] = list;

            if ((int) searchStatus == 0) {
                var (allCategories, allTotal) = this.CategoryService.GetAllCategories(pageIndex, pageSize, searchName);
                this.ViewData["categories"] = allCategories;
                this.ViewData["total"] = allTotal;
            }
            else {
                var (categories, total) = this.CategoryService.GetCategoriesAndCount(pageIndex, pageSize, searchName, searchStatus);
                this.ViewData["categories"] = categories;
                this.ViewData["total"] = total;
            }


            return View(RoutersAdmin.CategoryGetCategoryList.Page);
        }


        [HttpGet("update")]
        public IActionResult UpdateCategory(string categoryId) {

            var statusList = this.CategoryService.GetCategoryStatusDropList();
            statusList.Add(new SelectListItem() { Text = "All", Value = "" });
            SelectList statusSearchList = new SelectList(statusList, "");
            this.ViewData["statusSearch"] = statusSearchList;


            var category = this.CategoryService.GetCategoryByCategoryId(categoryId);
            SelectList list = new SelectList(this.CategoryService.GetCategoryStatusDropList(), "1");
            this.ViewData["status"] = list;
            this.ViewData["category"] = category;
            return View(RoutersAdmin.CategoryPutCategory.Page);
        }
    }
}
