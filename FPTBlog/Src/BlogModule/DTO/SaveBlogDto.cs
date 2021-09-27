
using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO {
    public class SaveBlogDto {
        public string BlogId {
            get; set;
        }
        public string Title {
            get; set;
        }
        public string Content {
            get; set;
        }
    }

    public class SaveBlogDtoValidator : AbstractValidator<SaveBlogDto> {
        public SaveBlogDtoValidator() {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(40);
            RuleFor(x => x.Content).NotEmpty().NotNull();
        }
    }
}
