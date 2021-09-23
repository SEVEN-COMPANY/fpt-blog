using FluentValidation;

namespace FPTBlog.Src.BlogModule.DTO
{
    public class PostBlogDto
    {
        public string BlogId { get; set; }
    }

    public class PostBlogDtoValidator : AbstractValidator<PostBlogDto>
    {
        public PostBlogDtoValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
        }
    }
}