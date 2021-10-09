using FluentValidation;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.PostModule.DTO {
    public class ApprovedPostDto {
        public string PostId {
            get; set;
        }

        public string Note {
            get; set;
        }

        public PostStatus Status {
            get; set;
        }
    }

    public class ApprovedPostDtoValidator : AbstractValidator<ApprovedPostDto> {
        public ApprovedPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
            RuleFor(x => x.Note).NotEmpty().MinimumLength(1).MaximumLength(500).NotNull();
        }
    }
}
