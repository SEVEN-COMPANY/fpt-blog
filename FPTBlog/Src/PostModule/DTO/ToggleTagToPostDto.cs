using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class ToggleTagToPostDto {
        public string PostId {
            get; set;
        }
        public string TagName {
            get; set;
        }
    }

    public class ToggleTagToPostDtoValidator : AbstractValidator<ToggleTagToPostDto> {
        public ToggleTagToPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
            RuleFor(x => x.TagName).NotEmpty().NotNull();
        }
    }
}
