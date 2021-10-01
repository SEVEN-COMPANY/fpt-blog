using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class UpdateCategoryOfPostDto {
        public string PostId {
            get; set;
        }
        public string CategoryId {
            get; set;
        }
    }

    public class UpdateCategoryOfPostDtoValidator : AbstractValidator<UpdateCategoryOfPostDto> {
        public UpdateCategoryOfPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        }
    }
}
