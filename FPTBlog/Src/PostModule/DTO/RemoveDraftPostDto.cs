using FluentValidation;
using FPTBlog.Src.PostModule.Entity;

namespace FPTBlog.Src.PostModule.DTO
{
    public class RemoveDraftPostDto
    {
        public string PostId {
            get; set;
        }

        public string UserId {
            get; set;
        }

    }

    public class RemoveDraftPostDtoValidator : AbstractValidator<RemoveDraftPostDto> {
        public RemoveDraftPostDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
        }
    }
}
