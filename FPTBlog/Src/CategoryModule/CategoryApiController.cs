
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.DTO;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Src.AuthModule;
using FPTBlog.Utils.Common;
using FluentValidation.Results;
using FPTBlog.Utils.Locale;

using System;

using FPTBlog.Src.BlogModule.Interface;


namespace FPTBlog.Src.CategoryModule
{
    [Route("/api/category")]
    // [ServiceFilter(typeof(AuthGuard))]

    public class CategoryApiController : Controller
    {

        private readonly ICategoryService CategoryService;
        private readonly IBlogService BlogService;


        public CategoryApiController(ICategoryService categoryService, IBlogService blogService)
        {
            this.BlogService = blogService;
            this.CategoryService = categoryService;
        }


        [HttpPost("")]
        public IActionResult HandleCreateCategory([FromBody] CreateCategoryDTO input)
        {

            var res = new ServerApiResponse<Category>();
            ValidationResult result = new CreateCategoryDTOValidator().Validate(input);
            if (!result.IsValid)
            {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isExistCategory = this.CategoryService.GetCategoryByCategoryName(input.Name);
            if (isExistCategory != null)
            {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "name");
                return new NotFoundObjectResult(res.getResponse());
            }

            var category = new Category();
            category.CategoryId = Guid.NewGuid().ToString();
            category.Name = input.Name;
            category.Description = input.Description;
            this.CategoryService.SaveCategory(category);

            res.data = category;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}