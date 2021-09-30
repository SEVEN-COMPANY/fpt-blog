using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class SendPostDto {
        public string PostId {
            get; set;
        }
    }

    public class SendPostDtoValidator : AbstractValidator<SendPostDto> {
        public SendPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
