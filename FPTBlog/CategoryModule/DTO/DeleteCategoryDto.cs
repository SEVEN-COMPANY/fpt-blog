using FluentValidation;
using FPTBlog.Utils.Validator;
using FPTBlog.CategoryModule.Entity;

namespace FPTBlog.CategoryModule.DTO
{
    public class DeleteCategoryDTO
    {
        public CategoryStatus Status { get; set; }
    }

    public class DeleteCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public DeleteCategoryDTOValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().NotNull().Length(CategoryValidator.CATEGORY_ID_MIN, CategoryValidator.CATEGORY_ID_MAX);
            RuleFor(x => x.Status).NotEmpty().NotNull().IsInEnum();
        }
    }
}
