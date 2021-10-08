using FluentValidation;


namespace FPTBlog.Src.UserModule.DTO {
    public class ToggleUserDto {
        public string UserId {
            get; set;
        }
    }
    public class ToggleUserDtoValidator : AbstractValidator<ToggleUserDto> {
        public ToggleUserDtoValidator() {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
        }
    }
}
