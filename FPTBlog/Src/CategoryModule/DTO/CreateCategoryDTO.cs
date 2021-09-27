using FluentValidation;
using FPTBlog.Utils.Validator;
using FPTBlog.Src.CategoryModule.Entity;

namespace FPTBlog.Src.CategoryModule.DTO {
    public class CreateCategoryDTO {
        public string Name {
            get; set;
        }
        public string Description {
            get; set;
        }
        public CategoryStatus Status {
            get; set;
        }
    }

    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO> {
        public CreateCategoryDTOValidator() {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(CategoryValidator.NAME_MIN, CategoryValidator.NAME_MAX);
            RuleFor(x => x.Description).NotEmpty().NotNull().Length(CategoryValidator.DESCRIPTION_MIN, CategoryValidator.DESCRIPTION_MAX);
            RuleFor(x => x.Status).NotEmpty().NotNull().IsInEnum();
        }
    }
}
