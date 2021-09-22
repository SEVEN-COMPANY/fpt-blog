using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO
{
    public class AddCategoryToBlogDto
    {
        public string BlogId { get; set; }
        public string CategoryId { get; set; }
    }

    public class AddCategoryToBlogDtoValidator : AbstractValidator<AddCategoryToBlogDto>
    {
        public AddCategoryToBlogDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        }
    }
}