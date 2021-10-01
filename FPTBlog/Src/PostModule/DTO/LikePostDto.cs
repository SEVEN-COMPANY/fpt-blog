using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class LikePostDto {
        public string PostId {
            get; set;
        }
    }

    public class LikepostDtoValidator : AbstractValidator<LikePostDto> {
        public LikepostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
