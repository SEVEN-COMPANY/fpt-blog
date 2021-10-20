using FluentValidation;

namespace FPTBlog.Src.ChatModule.DTO {
    public class AddMessageDTO {
        public string Content {
            get; set;
        }
        public string OwnerId {
            get; set;
        }
    }

    public class AddMessageDTOValidator : AbstractValidator<AddMessageDTO> {
        public AddMessageDTOValidator() {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.OwnerId).NotEmpty().NotNull();
        }
    }
}