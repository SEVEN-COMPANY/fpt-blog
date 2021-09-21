using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.DTO;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.AuthModule;
using FPTBlog.Utils.Common;
using FluentValidation.Results;
using FPTBlog.Utils.Locale;
using FPTBlog.Utils;
using System;
using FPTBlog.Src.BlogModule.Entity;
using FPTBlog.Src.BlogModule.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FPTBlog.Src.CategoryModule
{
    [Route("category")]
    [ServiceFilter(typeof(AuthGuard))]
    public class CategoryMvcController : Controller
    {
        private readonly ICategoryService CategoryService;
        private readonly IBlogService BlogService;


        public CategoryMvcController(ICategoryService categoryService, IBlogService blogService)
        {
            this.BlogService = blogService;
            this.CategoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult Category()
        {
            SelectList list = new SelectList(this.CategoryService.GetRadioStatusList(), CategoryStatus.ACTIVE.ToString());
            this.ViewData["status"] = list;

            return View(Routers.Category.Page);
        }

        [HttpGet("create")]
        public IActionResult AddCategoryPage()
        {
            SelectList list = new SelectList(this.CategoryService.GetRadioStatusList(), CategoryStatus.ACTIVE.ToString());
            this.ViewData["status"] = list;

            return View(Routers.CreateCategory.Page);
        }

        [HttpGet("update")]
        public IActionResult UpdateCategory(string categoryId)
        {
            var category = this.CategoryService.GetCategoryByCategoryId(categoryId);
            this.ViewData["category"] = category;
            return View(Routers.UpdateCategory.Page);
        }

        [HttpPost("update")]
        public IActionResult HandleUpdateCategory(string categoryId, string name, string description, CategoryStatus status)
        {
            var input = new UpdateCategoryDTO()
            {
                CategoryId = categoryId,
                Name = name,
                Description = description,
                Status = status
            };

            ValidationResult result = new UpdateCategoryDTOValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerMvcResponse.MapDetails(result, this.ViewData);
                return this.UpdateCategory(categoryId);
            }

            var category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null)
            {
                ServerMvcResponse.SetFieldErrorMessage("categoryId", CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return this.UpdateCategory(categoryId);
            }

            if (category.Name != input.Name)
            {
                var isExistCategory = this.CategoryService.GetCategoryByCategoryName(input.Name);
                if (isExistCategory != null)
                {
                    ServerMvcResponse.SetFieldErrorMessage("name", CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, this.ViewData);
                    return this.UpdateCategory(categoryId);
                }
            }

            category.Name = input.Name;
            category.Description = input.Description;
            category.Status = input.Status;

            this.CategoryService.UpdateCategory(category);

            ServerMvcResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS, this.ViewData);
            return Redirect(Routers.Category.Link);
        }

        [HttpGet("delete")]
        public IActionResult DeleteCategory(string categoryId)
        {
            var category = this.CategoryService.GetCategoryByCategoryId(categoryId);
            this.ViewData["category"] = category;
            return View(Routers.DeleteCategory.Page);
        }

        [HttpPost("delete")]
        public IActionResult DeleteCategoryHandler(string categoryId, CategoryStatus status)
        {
            var input = new UpdateCategoryDTO()
            {
                CategoryId = categoryId,
                Status = status
            };

            ValidationResult result = new UpdateCategoryDTOValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerMvcResponse.MapDetails(result, this.ViewData);
                return Redirect(Routers.Category.Page);
            }

            var category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null)
            {
                ServerMvcResponse.SetFieldErrorMessage("categoryId", CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return Redirect(Routers.Category.Page);
            }

            if (category.Status == 0)
            {
                ServerMvcResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW, this.ViewData);
                return Redirect(Routers.Category.Page);
            }
            category.Status = input.Status;

            this.CategoryService.DeleteCategory(category);

            ServerMvcResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS, this.ViewData);
            return Redirect(Routers.Category.Link);
        }

        [HttpPost("blog/add")]
        public string AddCategoryToBlog([FromBody] AddCategoryToBlogDto input)
        {
            Console.WriteLine(input.BlogId);
            ValidationResult result = new AddCategoryToBlogDtoValidator().Validate(input);
            if (!result.IsValid)
            {
                return "not pass validation";
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if (blog == null)
            {

                return "blog not found";
            }

            Category category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null)
            {

                return "category not found";
            }

            blog.CategoryId = input.CategoryId;
            this.BlogService.UpdateBlog(blog);

            return "ok";
        }
    }
}