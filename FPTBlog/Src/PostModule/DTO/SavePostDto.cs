
using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class SavePostDto {
        public string PostId {
            get; set;
        }
        public string Title {
            get; set;
        }
        public string Description {
            get; set;
        }
        public string Content {
            get; set;
        }
        public string CoverUrl {
            get; set;
        }
        public int ReadTime {
            get; set;
        }
    }

    public class SaveBlogDtoValidator : AbstractValidator<SavePostDto> {
        public SaveBlogDtoValidator() {
            RuleFor(x => x.PostId).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.CoverUrl).NotEmpty().NotNull();
            RuleFor(x => x.ReadTime).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
        }
    }
}
