using FluentValidation;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.NotificationModule.DTO {
    public class AddNotificationUserDTO {


        public string Description {
            get; set;
        }

        public string Username {
            get; set;
        }
    }

    public class AddNotificationUserDTOValidator : AbstractValidator<AddNotificationUserDTO> {
        public AddNotificationUserDTOValidator() {
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Username).NotEmpty().NotNull();
        }
    }
}
