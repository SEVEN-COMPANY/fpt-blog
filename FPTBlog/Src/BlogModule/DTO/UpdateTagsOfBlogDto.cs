using System.Collections.Generic;
using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO {
    public class UpdateTagsOfBlogDto {
        public string BlogId {
            get; set;
        }
        public List<string> Tags {
            get; set;
        }
    }

    public class UpdateTagsOfBlogDtoValidator : AbstractValidator<UpdateTagsOfBlogDto> {
        public UpdateTagsOfBlogDtoValidator() {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
            RuleFor(x => x.Tags).NotEmpty().NotNull();
        }
    }
}
