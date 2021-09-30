using FluentValidation;

namespace FPTBlog.Src.CommentModule.DTO {
    public class UpdateCommentDto {
        public string CommentId {
            get; set;
        }
        public string Content {
            get; set;
        }
        public string BlogId {
            get; set;
        }
    }
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto> {
        public UpdateCommentDtoValidator() {
            RuleFor(x => x.CommentId).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.BlogId).NotEmpty().NotNull();
        }
    }
}
