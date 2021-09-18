using Microsoft.AspNetCore.Mvc;
using FPTBlog.CategoryModule.Interface;
using FPTBlog.CategoryModule.DTO;
using FPTBlog.CategoryModule.Entity;
using FPTBlog.AuthModule;
using FPTBlog.Utils.Common;
using FluentValidation.Results;
using FPTBlog.Utils.Locale;
using FPTBlog.Utils;
using System;
using FPTBlog.BlogModule.Entity;
using FPTBlog.BlogModule.Interface;

namespace CategoryModule.Controllers
{
    [Route("category")]
    [ServiceFilter(typeof(AuthGuard))]
    public class CategoryMvcController : Controller
    {
        private readonly ICategoryService CategoryService;
        private readonly IBlogService BlogService;
        private readonly DB DB;

<<<<<<< HEAD:FPTBlog/CategoryModule/CategoryMvcController.cs
        public CategoryMvcController(ICategoryService categoryService, DB dB)
=======
        public CategoryController(IBlogService blogService, ICategoryService categoryService, DB dB)
>>>>>>> 1d09fb12ce8f0288164d657b143877db7223dad4:FPTBlog/CategoryModule/CategoryController.cs
        {
            this.BlogService = blogService;
            this.CategoryService = categoryService;
            this.DB = dB;
        }

        [HttpGet("")]
        public IActionResult Category()
        {
            var categories = this.CategoryService.GetCategories();

            this.ViewData["categories"] = categories;
            return View(Routers.Category.Page);
        }

        [HttpGet("create")]
        public IActionResult AddCategoryPage()
        {
            return View(Routers.CreateCategory.Page);
        }

        [HttpPost("create")]
        public IActionResult HandleCreateCategory(string name, string description, CategoryStatus status)
        {
            var input = new CreateCategoryDTO()
            {
                Name = name,
                Description = description,
                Status = status
            };

            ValidationResult result = new CreateCategoryDTOValidator().Validate(input);
            if (!result.IsValid)
            {
                ServerResponse.MapDetails(result, this.ViewData);
                return View(Routers.CreateCategory.Page);
            }

            var isExistCategory = this.CategoryService.GetCategoryByCategoryName(input.Name);
            if (isExistCategory != null)
            {
                ServerResponse.SetFieldErrorMessage("name", CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, this.ViewData);
                return View(Routers.CreateCategory.Page);
            }

            var category = new Category();
            category.CategoryId = Guid.NewGuid().ToString();
            category.Name = input.Name;
            category.Description = input.Description;
            category.Status = input.Status;
            category.CreateDate = DateTime.Now.ToShortDateString();
            this.CategoryService.SaveCategory(category);

            ServerResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS, this.ViewData);
            return Redirect(Routers.Category.Link);
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
                ServerResponse.MapDetails(result, this.ViewData);
                return this.UpdateCategory(categoryId);
            }

            var category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null)
            {
                ServerResponse.SetFieldErrorMessage("categoryId", CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return this.UpdateCategory(categoryId);
            }

            if (category.Name != input.Name)
            {
                var isExistCategory = this.CategoryService.GetCategoryByCategoryName(input.Name);
                if (isExistCategory != null)
                {
                    ServerResponse.SetFieldErrorMessage("name", CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, this.ViewData);
                    return this.UpdateCategory(categoryId);
                }
            }

            category.Name = input.Name;
            category.Description = input.Description;
            category.Status = input.Status;

            this.CategoryService.UpdateCategory(category);

            ServerResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS, this.ViewData);
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
                ServerResponse.MapDetails(result, this.ViewData);
                return Redirect(Routers.Category.Page);
            }

            var category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if (category == null)
            {
                ServerResponse.SetFieldErrorMessage("categoryId", CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, this.ViewData);
                return Redirect(Routers.Category.Page);
            }

            if (category.Status==0)
            {
                ServerResponse.SetErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW, this.ViewData);
                return Redirect(Routers.Category.Page);
            }
            category.Status = input.Status;

            this.CategoryService.DeleteCategory(category);

            ServerResponse.SetMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS, this.ViewData);
            return Redirect(Routers.Category.Link);
        }

        [HttpPost("blog/add")]
        public string AddCategoryToBlog([FromBody] AddCategoryToBlogDto input){
            Console.WriteLine(input.BlogId);
            ValidationResult result = new AddCategoryToBlogDtoValidator().Validate(input);
            if(!result.IsValid){
                return "not pass validation";
            }

            Blog blog = this.BlogService.GetBlogByBlogId(input.BlogId);
            if(blog == null){

                return "blog not found";
            }

            Category category = this.CategoryService.GetCategoryByCategoryId(input.CategoryId);
            if(category == null){

                return "category not found";
            }

            blog.CategoryId = input.CategoryId;
            this.BlogService.UpdateBlog(blog);
            
            return "ok";
        }
    }
}