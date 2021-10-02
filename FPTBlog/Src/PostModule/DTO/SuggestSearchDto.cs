using FluentValidation;

namespace FPTBlog.Src.PostModule.DTO {
    public class SuggestSearchDto {
        public string Search {
            get; set;
        }

        public string CategoryId {
            get; set;
        }
    }

    public class SuggestSearchDtoValidator : AbstractValidator<SuggestSearchDto> {
        public SuggestSearchDtoValidator() {
            RuleFor(x => x.Search).NotEmpty().NotNull();
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        }
    }
}