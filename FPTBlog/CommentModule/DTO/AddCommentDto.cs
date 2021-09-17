using FluentValidation;

namespace FPTBlog.CommentModule.DTO
{
    public class AddCommentDto
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public string BlogId { get; set; }
    }

    public class AddCommentDtoValidator : AbstractValidator<AddCommentDto>
    {
        public AddCommentDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.Content).NotNull().NotEmpty();
            RuleFor(x => x.BlogId).NotNull().NotEmpty();
        }
    }
}