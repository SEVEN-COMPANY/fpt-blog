using FluentValidation;

namespace FPTBlog.Src.CategoryModule.DTO
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
            RuleFor(x => x.BlogId).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();
        }
    }
}