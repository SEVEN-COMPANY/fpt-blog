using FluentValidation;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.PostModule.DTO {
    public class ChangeStatusPostDto {
        public string PostId {
            get; set;
        }
        public PostStatus Status {
            get; set;
        }
    }

    public class ChangeStatusPostDtoValidator : AbstractValidator<ChangeStatusPostDto> {
        public ChangeStatusPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
        }
    }
}