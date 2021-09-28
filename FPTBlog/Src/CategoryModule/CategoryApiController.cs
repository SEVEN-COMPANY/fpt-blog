using Microsoft.AspNetCore.Mvc;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule.DTO;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Utils.Common;
using FluentValidation.Results;
using FPTBlog.Utils.Locale;
using System.Collections.Generic;

using System;

using FPTBlog.Src.BlogModule.Interface;


namespace FPTBlog.Src.CategoryModule {
    [Route("/api/category")]
    // [ServiceFilter(typeof(AuthGuard))]

    public class CategoryApiController : Controller {

        private readonly ICategoryService CategoryService;
        private readonly IBlogService BlogService;


        public CategoryApiController(ICategoryService categoryService, IBlogService blogService) {
            this.BlogService = blogService;
            this.CategoryService = categoryService;
        }

        // [HttpGet("")]
        // public ObjectResult GetCategories() {
        //     var res = new ServerApiResponse<List<Category>>();
        //     List<Category> list = this.CategoryService.GetCategories();
        //     res.data = list;
        //     return new ObjectResult(res.getResponse());
        // }

        [HttpPost("")]
        public ObjectResult HandleCreateCategory([FromBody] CreateCategoryDTO body) {

            var res = new ServerApiResponse<Category>();
            ValidationResult result = new CreateCategoryDTOValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var isExistCategory = this.CategoryService.GetCategoryByCategoryName(body.Name);
            if (isExistCategory != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "name");
                return new BadRequestObjectResult(res.getResponse());
            }

            var category = new Category();
            category.CategoryId = Guid.NewGuid().ToString();
            category.Name = body.Name;
            category.Description = body.Description;
            category.Status = body.Status;
            category.CreateDate = DateTime.Now.ToShortDateString();
            this.CategoryService.SaveCategory(category);

            res.data = category;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("update")]
        public ObjectResult HandleUpdateCategory([FromBody] UpdateCategoryDTO body) {
            var res = new ServerApiResponse<string>();

            ValidationResult result = new UpdateCategoryDTOValidator().Validate(body);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var category = this.CategoryService.GetCategoryByCategoryId(body.CategoryId);
            if (category == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "categoryId");
                return new NotFoundObjectResult(res.getResponse());
            }

            if (category.Name != body.Name) {
                var isExistCategory = this.CategoryService.GetCategoryByCategoryName(body.Name);
                if (isExistCategory != null) {
                    res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "name");
                    return new BadRequestObjectResult(res.getResponse());
                }
            }

            category.Name = body.Name;
            category.Description = body.Description;
            category.Status = body.Status;

            this.CategoryService.UpdateCategory(category);

            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }
    }
}
