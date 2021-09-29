using System.Collections.Generic;
using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO {
    public class ToggleTagToBlogDto {
        public string BlogId {
            get; set;
        }
        public string TagName {
            get; set;
        }
    }

    public class ToggleTagToBlogDtoValidator : AbstractValidator<ToggleTagToBlogDto> {
        public ToggleTagToBlogDtoValidator() {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
            RuleFor(x => x.TagName).NotEmpty().NotNull();
        }
    }
}
