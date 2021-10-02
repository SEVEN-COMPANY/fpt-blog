using FluentValidation;

namespace FPTBlog.Src.CommentModule.DTO {
    public class RemoveCommentDto {
        public string CommentId {
            get; set;
        }
    }
    public class RemoveCommentDtoValidator : AbstractValidator<RemoveCommentDto> {
        public RemoveCommentDtoValidator() {
            RuleFor(x => x.CommentId).NotEmpty().NotNull();
        }
    }
}
