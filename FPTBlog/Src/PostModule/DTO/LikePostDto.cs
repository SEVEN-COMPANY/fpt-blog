using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class LikePostDto {
        public string PostId {
            get; set;
        }
    }

    public class LikeBlogDtoValidator : AbstractValidator<LikePostDto> {
        public LikeBlogDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
