using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO {
    public class LikeBlogDto {
        public string BlogId {
            get; set;
        }
    }

    public class LikeBlogDtoValidator : AbstractValidator<LikeBlogDto> {
        public LikeBlogDtoValidator() {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
        }
    }
}