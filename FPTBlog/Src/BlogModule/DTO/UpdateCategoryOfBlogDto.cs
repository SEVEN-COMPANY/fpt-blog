using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO
{
    public class UpdateCategoryOfBlogDto
    {
        public string BlogId { get; set; }
        public string CategoryId { get; set; }
    }

    public class UpdateCategoryOfBlogDtoValidator : AbstractValidator<UpdateCategoryOfBlogDto>
    {
        public UpdateCategoryOfBlogDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        }
    }
}