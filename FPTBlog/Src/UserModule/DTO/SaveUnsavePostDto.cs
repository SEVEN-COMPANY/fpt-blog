using FluentValidation;


namespace FPTBlog.Src.UserModule.DTO {
    public class SaveUnsavePostDto {
        public string PostId {
            get; set;
        }
    }
    public class SaveUnsavePostDtoValidator : AbstractValidator<SaveUnsavePostDto> {
        public SaveUnsavePostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}
