using FluentValidation;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.UserModule.DTO {
    public class ToggleUserDto {
        public string UserId {
            get; set;
        }
        public string Description {
            get; set;
        }
        public string Content {
            get; set;
        }


        public NotificationLevel Level {
            get; set;
        }
    }
    public class ToggleUserDtoValidator : AbstractValidator<ToggleUserDto> {
        public ToggleUserDtoValidator() {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.Level).NotEmpty().NotNull().IsInEnum();
        }
    }
}
