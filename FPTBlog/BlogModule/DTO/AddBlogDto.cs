using FluentValidation;

namespace FPTBlog.BlogModule.DTO
{
    public class AddBlogDto
    {
        public string BlogId {get;set;}
        public string Title {get;set;}
        public string Content {get;set;}
    }

    public class AddBlogDtoValidator: AbstractValidator<AddBlogDto>
    {
        public AddBlogDtoValidator(){
            RuleFor(x => x.BlogId).NotNull().NotEmpty();
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(x => x.Content).NotNull().NotEmpty();
        }
    }
}