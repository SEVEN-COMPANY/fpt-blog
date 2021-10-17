using FluentValidation;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Src.NotificationModule.DTO {
    public class AddNotificationDTO {
        public string Content {
            get; set;
        }
        public string Description {
            get; set;
        }

        public NotificationLevel Level {
            get; set;
        }

        public string ReceiverId {
            get; set;
        }
    }

    public class AddNotificationDTOValidator : AbstractValidator<AddNotificationDTO> {
        public AddNotificationDTOValidator() {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Level).NotEmpty().NotNull().IsInEnum();
            RuleFor(x => x.ReceiverId).NotNull().NotEmpty();
        }
    }
}