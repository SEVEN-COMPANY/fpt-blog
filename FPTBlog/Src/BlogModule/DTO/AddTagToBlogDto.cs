using System.Collections.Generic;
using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO
{
    public class AddTagToBlogDto
    {
        public string BlogId { get; set; }
        public List<string> Tags{get;set;}
    }

    public class AddTagToBlogDtoValidator : AbstractValidator<AddTagToBlogDto>
    {
        public AddTagToBlogDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
            RuleFor(x => x.Tags).NotEmpty().NotNull();
        }
    }
}