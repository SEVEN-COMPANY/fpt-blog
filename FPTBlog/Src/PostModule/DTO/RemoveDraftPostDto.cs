using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class RemoveDraftPostDto {
        public string PostId {
            get; set;
        }
    }

    public class RemoveDraftPostDtoValidator : AbstractValidator<RemoveDraftPostDto> {
        public RemoveDraftPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
